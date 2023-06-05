using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Services
{
    public class BookFilterService
    {
        public IBookRepository BookRepository { get; }
        public BookFilterService(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public async Task<List<Book>> GetFilterBooksAsync(BookFilter bookFilter)
        {
            return await BookRepository.GetFilteredBooksAsync(bookFilter);
        }


    }
}
