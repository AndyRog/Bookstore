using Bookstore.Application.Dtos;

namespace Bookstore.Application.Contracts;

public interface IAuthorRepository
{
    Task<long> AddAuthor(AuthorCreate author);
}