using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Application.Contracts;

public interface IAuthorRepository
{
    Task<long> AddAuthorAsync(Author author);
    
    Task<Author?> GetAuthorById(long authorId);
    Task Update();
}