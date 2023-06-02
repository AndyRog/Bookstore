using Bookstore.Domain.Entities;

namespace Bookstore.Application.Contracts;

public interface IBookRepository
{
    Task<long> AddBook(Book book);
    Task<Book?> GetBookId(long bookId);
    Task<Book?> GetBookIsbn(string iSBN);
    Task Update();
}