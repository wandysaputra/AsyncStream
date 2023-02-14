using AsyncStream.Entities;
using AsyncStream.Filters.ResultFilters;
using AsyncStream.Models.ApiDtos;
using AsyncStream.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AsyncStream.Controllers;

[Route("api")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IMapper _mapper;

    public BooksController(IBooksRepository booksRepository, IMapper mapper)
    {
        _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        _mapper = mapper;
    }

    [HttpGet("books")]
    [TypeFilter(typeof(BooksResultFilter))]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _booksRepository.GetBooksAsync();

        return Ok(books);
    }

    [HttpGet("books/{id:guid}", Name = "GetBook")]
    // [TypeFilter(typeof(BookResultFilterAttribute))] // with ResultFilterAttribute
    [TypeFilter(typeof(BookResultFilter))] // with IAsyncResultFilter
    public async Task<IActionResult> GetBooks(Guid id)
    {
        var bookEntity = await _booksRepository.GetBookAsync(id);
        if (bookEntity == null)
        {
            return NotFound();
        }

        return Ok(bookEntity);
    }

    [HttpPost("books")]
    [TypeFilter(typeof(BookResultFilter))] // to convert bookEntity to BookDto on CreatedAtRoute
    public async Task<IActionResult> CreateBook([FromBody] BookForCreationDto bookForCreationDto)
    {
        var bookEntity = _mapper.Map<Book>(bookForCreationDto);
        _booksRepository.AddBook(bookEntity);

        var saveChanges = await _booksRepository.SaveChangesAsync();
        
        await _booksRepository.GetBookAsync(bookEntity.Id); // to populate author full name

        return saveChanges ? CreatedAtRoute("GetBook", new {id = bookEntity.Id}, bookEntity) : Problem();
    }

}