using AsyncStream.Entities;

namespace AsyncStream.Services;

public interface IBooksRepository
{
    IEnumerable<Entities.Book> GetBooks();
    Entities.Book? GetBook(Guid id);

    Task<IEnumerable<Entities.Book>> GetBooksAsync();
    Task<Entities.Book?> GetBookAsync(Guid id);
    void AddBook(Entities.Book bookToAdd);
    Task<bool> SaveChangesAsync();
    Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> booksIds);
}

