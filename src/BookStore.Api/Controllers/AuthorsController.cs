﻿using System;
using System.Threading.Tasks;
using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    public class AuthorsController : BaseApiController
    {
        private readonly ILoggerService _logger;
        private readonly IAuthorCache _authorCache;
        private readonly IUnitOfWork _unitOfWork;

        public AuthorsController(ILoggerService logger, IAuthorCache authorCache, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _authorCache = authorCache;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get All Authors as a list of JSON response
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAuthors(string search)
        {
            var location = GetControllerActionNames();

            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    _logger.LogInformation($"{location}: Attempting to get all Author records...");

                    var authors = await _unitOfWork.AuthorRepository.FindAll();

                    _logger.LogInformation($"{location}: Successfully got all Author records");

                    return Ok(authors);
                }
                else
                {
                    _logger.LogInformation($"{location}: Attempting to get Authors with search parameter of: { search }");

                    var searchAuthor = await _unitOfWork.AuthorRepository.FindBySearch(search);

                    _logger.LogInformation($"{location}: Successfully returned Authors with search parameter of: { search }");

                    return Ok(searchAuthor);
                }
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
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
                _logger.LogInformation($"{location}: Attempting to get a single record with id:{id}");

                var author = _authorCache.Get(id);

                if (author == null)
                {
                    author = await _unitOfWork.AuthorRepository.FindById(id);

                    if (author == null)
                    {
                        _logger.LogWarning($"{location}: Record with id:{id} was not found");

                        return NotFound();
                    }

                    _authorCache.Set(author);
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

                var isSuccess = await _unitOfWork.AuthorRepository.Create(author);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Record creation failed");
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
                    _logger.LogWarning($"{location}: Record update failed with invalid data");

                    return BadRequest();
                }

                var ifExists = await _unitOfWork.AuthorRepository.IsExists(id);

                if (ifExists == false)
                {
                    _logger.LogWarning($"{location}: Record with id: {id} was not found");

                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isSuccess = await _unitOfWork.AuthorRepository.Update(authorToUpdate);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Update operation failed");
                }

                _authorCache.Remove(id);

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
                _logger.LogInformation($"Author deletion attempted with record id: {id}");

                if(id < 1)
                {
                    _logger.LogWarning($"{location}: Record deletion failed with invalid data");

                    return BadRequest();
                }

                var author = await _unitOfWork.AuthorRepository.FindById(id);

                if (author == null)
                {
                    _logger.LogWarning($"{location}: Record with id: {id} was not found");

                    return NotFound();
                }

                 var isSuccess = await _unitOfWork.AuthorRepository.Delete(author);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Delete operation failed");
                }

                _authorCache.Remove(id);

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
