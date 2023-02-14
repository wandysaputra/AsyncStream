using AsyncStream.Entities;
using AsyncStream.Models.ApiDtos;
using AutoMapper;

namespace AsyncStream.AutoMapperProfiles;

public class BooksProfile : Profile
{
    public BooksProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(destinationMember => destinationMember.AuthorName
                , memberOptions => memberOptions
                    .MapFrom(source => $"{source.Author.FirstName} {source.Author.LastName}"))
            .ConstructUsing(source => new BookDto(source.Id, 
                string.Empty, 
                source.AuthorId,
                source.Title, 
                source.Description));

        CreateMap<BookForCreationDto, Book>();
        // alternative way via automapper profile instead of create override method of construction
        // .ConstructUsing(source => new Book(Guid.NewGuid(), source.AuthorId, source.Title, source.Description);
    }
}