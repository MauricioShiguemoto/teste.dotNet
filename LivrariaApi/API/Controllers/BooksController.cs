using LivrariaApi.Application.Interfaces;
using LivrariaApi.API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaApi.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody] CreateBookDto createBookDto)
        {
            var (book, errorMessage) = await _bookService.CreateBookAsync(
                createBookDto.Title,
                createBookDto.Author,
                createBookDto.Genre,
                createBookDto.Price,
                createBookDto.StockQuantity
            );

            if (book == null)
            {
                return Conflict(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            var (success, errorMessage) = await _bookService.UpdateBookAsync(
                id,
                updateBookDto.Title,
                updateBookDto.Author,
                updateBookDto.Genre,
                updateBookDto.Price,
                updateBookDto.StockQuantity
            );

            if (!success)
            {
                return NotFound(new { message = errorMessage });
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _bookService.DeleteBookAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}