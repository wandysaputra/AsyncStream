using AsyncStream.DbContexts;
using AsyncStream.Entities;
using Microsoft.EntityFrameworkCore;

namespace AsyncStream.Services;

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

    public void AddBook(Book bookToAdd)
    {
        if (bookToAdd == null)
        {
            throw new ArgumentNullException(nameof(bookToAdd));
        }

        // This method is async only to allow special value generators, such as the one used by
        // 'Microsoft.EntityFrameworkCore.Metadata.SqlServerValueGenerationStrategy.SequenceHiLo',
        // to access the database asynchronously. For all other cases the non async method should be used.
        // _context.AddAsync(bookToAdd);

        _context.Add(bookToAdd); // This not an I/O bound as this just add entity to the context to be tracked
    }
    public async Task<bool> SaveChangesAsync()
    {
        // return true if 1 or more entities were changed
        return (await _context.SaveChangesAsync() > 0);
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

    public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> booksIds)
    {
        return await _context.Books
            .Where(w => booksIds.Contains(w.Id))
            .Include(b => b.Author)
            .ToListAsync();
    }
}
