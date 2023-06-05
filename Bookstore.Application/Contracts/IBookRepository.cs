using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;

namespace Bookstore.Application.Contracts;

public interface IBookRepository
{
    Task<long> AddBookAsync(Book book);
    Task<Book?> GetBookByIdAsync(long bookId);
    Task<Book?> GetBookByIsbnAsync(string iSBN);
    Task<List<Book>> GetFilteredBooksAsync(BookFilter bookFilter);
    Task UpdateAsync();
}