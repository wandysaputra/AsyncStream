using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncStream.Entities;

[Table("Books")]
public class Book
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; }

    [MaxLength(2500)]
    public string? Description { get; set; }

    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = null!;

    public Book(Guid id, Guid authorId, string title, string? description)
    {
        Id = id;
        AuthorId = authorId;
        Title = title;
        Description = description;
    }

    // for BookForCreationDto mapper, by right with the [Key] attribute, ef core will put new value on every insert.
    public Book(Guid authorId, string title, string? description): this(Guid.NewGuid(), authorId, title, description)
    {
    }
}