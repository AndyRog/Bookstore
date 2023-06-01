using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Application.Contracts;

public interface IAuthorRepository
{
    Task<long> AddAuthor(Author author);
}