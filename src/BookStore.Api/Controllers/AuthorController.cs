using System;
using System.Threading.Tasks;
using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    public class AuthorController : BaseApiController
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly ILoggerService _logger;

        public AuthorController(IAuthorRepository authorRepo, ILoggerService logger)
        {
            _authorRepo = authorRepo;
            _logger = logger;
        }

        /// <summary>
        /// Get All Authors as a list of JSON response
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                _logger.LogInformation("Attempting to get all authors...");

                var authors = await _authorRepo.FindAll();

                _logger.LogInformation("Successfully got all Authors");

                return Ok(authors);
            }
            catch (Exception ex)
            {
                return InternalError($"{ex.Message} - {ex.InnerException}");
            }
            
        }

        /// <summary>
        /// Get a Single Author with a specific Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Attempting to get a single record with id:{id}...");

                var author = await _authorRepo.FindById(id);

                if(author == null)
                {
                    _logger.LogWarning($"{location}: Record with id:{id} was not found");
                    return NotFound();
                }

                _logger.LogInformation($"{location}: Successfully retrieved the Author with id:{id}");

                return Ok(author);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Create an Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Attempting to submit a single record");

                if(author == null)
                {
                    _logger.LogWarning($"{location}: Empty request was submitted");
                    return BadRequest(ModelState);
                }

                if(!ModelState.IsValid)
                {
                    _logger.LogWarning($"{location}: Data was incomplete");
                    return BadRequest(ModelState);
                }

                var isSuccess = await _authorRepo.Create(author);

                if(!isSuccess)
                {
                    return InternalError("{location}: Record creation failed");
                }

                _logger.LogInformation($"Successfully submitted an Author object as: {author}");
                return Created("CreateAuthor", new { author });
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Update An Author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="authorToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Author authorToUpdate)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"Author update attempted - id: {id}");

                if(id < 1 || authorToUpdate == null || id != authorToUpdate.Id)
                {
                    _logger.LogWarning($"{location}: Record deletion failed with invalid data");
                    return BadRequest();
                }

                var ifExists = await _authorRepo.IsExists(id);

                if(ifExists == false)
                {
                    _logger.LogWarning($"{location}: Record with id:{id} was not found");
                    return NotFound();
                }

                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isSuccess = await _authorRepo.Update(authorToUpdate);

                if(!isSuccess)
                {
                    return InternalError($"{location}: Update operation failed");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Delete An Author
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"Author deletion attempted - id: {id}");

                if(id < 1)
                {
                    _logger.LogWarning($"{location}: Record deletion failed with invalid data");
                    return BadRequest();
                }

                var author = await _authorRepo.FindById(id);

                if(author == null)
                {
                    _logger.LogWarning($"{location}: Record with id:{id} was not found");
                    return NotFound();
                }

                var isSuccess = await _authorRepo.Delete(author);

                if(!isSuccess)
                {
                    return InternalError($"{location}: Delete operation failed");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: { ex.Message} - { ex.InnerException}");
            }
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator."); 
        }
        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }
    }
}
