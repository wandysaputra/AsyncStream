using Books.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AsyncStream.Controllers
{
    [Route("api")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _booksRepository.GetBooksAsync();

            return Ok(books);
        }

        [HttpGet("books/{id:guid}")]
        public async Task<IActionResult> GetBooks(Guid id)
        {
            var bookEntity = await _booksRepository.GetBookAsync(id);
            if (bookEntity == null)
            {
                return NotFound();
            }

            return Ok(bookEntity);
        }
    }
}
