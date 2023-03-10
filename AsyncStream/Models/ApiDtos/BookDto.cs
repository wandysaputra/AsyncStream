namespace AsyncStream.Models.ApiDtos;

public class BookDto
{
    public Guid Id { get; set; }
    public string AuthorName { get; set; }
    public Guid AuthorId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }

    public BookDto(Guid id, string authorName, Guid authorId, string title, string? description)
    {
        Id = id;
        AuthorName = authorName;
        AuthorId = authorId;
        Title = title;
        Description = description;
    }
}