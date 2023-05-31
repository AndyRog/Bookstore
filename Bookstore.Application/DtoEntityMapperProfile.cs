using AutoMapper;
using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Application;

public class DtoEntityMapperProfile : Profile
{
    public DtoEntityMapperProfile()
    {
        CreateMap<BookCreate, Book>().ForMember(destinationMember => destinationMember.Id, options => options.Ignore()).ForMember(destinationMember => destinationMember.Author, options => options.Ignore());
    }
}