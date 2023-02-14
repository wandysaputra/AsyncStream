using AsyncStream.Models.ApiDtos;
using AutoMapper;
using Books.API.Entities;

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
                source.Title, 
                source.Description));
    }
}