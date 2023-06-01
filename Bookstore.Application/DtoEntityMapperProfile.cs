using AutoMapper;
using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Application;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<BookCreate, Book>().ForMember(destinationMember => destinationMember.Id, options => options.Ignore()).ForMember(destinationMember => destinationMember.Author, options => options.Ignore());

        CreateMap<BookUpdate, Book>().ForMember(destinationMember => destinationMember.Id, options => options.Ignore()).ForMember(destinationMember => destinationMember.Quantity, options => options.Ignore()).ForMember(destinationMember => destinationMember.Author, options => options.Ignore());

        CreateMap<AuthorCreate, Author>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Books, opt => opt.Ignore());
        
        CreateMap<AuthorUpdate, Author>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Books, opt => opt.Ignore());
    }
}