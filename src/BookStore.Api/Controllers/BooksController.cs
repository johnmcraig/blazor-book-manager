using System;
using System.Threading.Tasks;
using BookStore.Core.Entities;
using BookStore.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    public class BooksController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookRepository _bookRepo;
        private readonly ILoggerService _logger;

        public BooksController(IBookRepository bookRepo, IUnitOfWork unitOfWork, ILoggerService logger)
        {
            _bookRepo = bookRepo;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get a list of all Books as a JSON response
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBooks(string search)
        {
            var location = GetControllerActionNames();

            try
            {
                if (string.IsNullOrEmpty(search))
                {
                    _logger.LogInformation($"{location}: Attempting to retrieve a list of records...");

                    // var books = await _unitOfWork.Repository<Book>().FindAll();
                    var books = await _bookRepo.FindAll();

                    _logger.LogInformation($"{location}: Successfully returned a list of records");

                    return Ok(books);
                }
                else
                {
                    _logger.LogInformation($"{location}: Attempting to get books with search parameter of: { search }");

                    var searchBook = await _bookRepo.FindBySearch(search);

                    _logger.LogInformation($"{location}: Successfully got books with search parameter of: { search }");

                    return Ok(searchBook);
                }
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: { ex.Message} - { ex.InnerException}");
            }
            
        }

        /// <summary>
        /// Get a Single Book with a specific Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBook(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Attempting to get a single book with id: {id}");

                // var book = await _unitOfWork.Repository<Book>().FindById(id);
                var book = await _bookRepo.FindById(id);

                if (book == null)
                {
                    _logger.LogWarning($"{location}: Failed to retrieve record with id: {id}");

                    return NotFound();
                }

                _logger.LogInformation($"{location}: Successfully retrieved the record with id: {id}");

                return Ok(book);
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: { ex.Message} - { ex.InnerException}");
            }
        }

        /// <summary>
        /// Create A Book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Create action attempted");

                if (book == null)
                {
                    _logger.LogWarning($"{location}: Empty request was submitted");

                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"{location}: Data was incomplete");

                    return BadRequest(ModelState);
                }

                // var isSuccess = await _unitOfWork.Repository<Book>().Create(book);
                var isSuccess = await _bookRepo.Create(book);

                if (!isSuccess)
                {
                    return InternalError($"{location}: record creation failed");
                }

                _logger.LogInformation($"{location}: Successfully submitted a record as: {book}");

                return Created("CreateBook", new { book });
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: { ex.Message} - { ex.InnerException}");
            }
        }

        /// <summary>
        /// Update A Book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookToUpdate"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book bookToUpdate)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Record update attempted with id: {id}");

                if (id < 1 || bookToUpdate == null || id != bookToUpdate.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // var isSuccess = await _unitOfWork.Repository<Book>().Update(bookToUpdate);
                var isSuccess = await _bookRepo.Update(bookToUpdate);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Update operation failed");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return InternalError($"{location}: { ex.Message} - { ex.InnerException}");
            }
        }

        /// <summary>
        /// Delete A Book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var location = GetControllerActionNames();

            try
            {
                _logger.LogInformation($"{location}: Record deletion attempted with id: {id}");

                if (id < 1)
                {
                    _logger.LogWarning($"{location}: Record deletion failed with invalid data");

                    return BadRequest();
                }

                // var book = await _unitOfWork.Repository<Book>().FindById(id);
                var book = await _bookRepo.FindById(id);

                if (book == null)
                {
                    _logger.LogWarning($"{location}: Record with id:{id} was not found");

                    return NotFound();
                }

                // var isSuccess = await _unitOfWork.Repository<Book>().Delete(book);
                var isSuccess = await _bookRepo.Delete(book);

                if (!isSuccess)
                {
                    return InternalError($"{location}: Update operation failed");
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
