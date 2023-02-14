using Books.API.DbContexts;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Services;

public class BooksRepository : IBooksRepository
{
    private readonly BooksContext _context;

    public BooksRepository(BooksContext context)
    {
        _context = context ?? 
            throw new ArgumentNullException(nameof(context));
    }

    public async Task<Book?> GetBookAsync(Guid id)
    {
        return await _context.Books
            .Include(i => i.Author)
            .FirstOrDefaultAsync(b => b.Id == id); 
    }

    public IEnumerable<Book> GetBooks()
    {
        return _context.Books
            .Include(i => i.Author)
            .ToList();
    }

    public Book? GetBook(Guid id)
    {
        return _context.Books
            .Include(i => i.Author)
            .FirstOrDefault(b => b.Id == id);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _context.Books
            .Include(b => b.Author)
            .ToListAsync();
    }
}
