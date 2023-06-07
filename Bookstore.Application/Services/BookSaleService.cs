using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Services;

public class BookSaleService
{
    public IBookRepository BookRepository { get; }
    public BookSaleValidator BookSaleValidator { get; }
    public BookValidator BookValidator { get; }

    public BookSaleService(IBookRepository bookRepository, BookSaleValidator bookSaleValidator, BookValidator bookValidator)
    {
        BookRepository = bookRepository;
        BookSaleValidator = bookSaleValidator;
        BookValidator = bookValidator;
    }
   
    
    public async Task ProcessBookSaleAsync(BookSale bookSale)
    {
        await BookSaleValidator.ValidateAndThrowAsync(bookSale);

        Book? book = await BookRepository.GetBookByIdAsync(bookSale.BookId);
        if (book == null)
        {
            throw new BookNotFoundException();
        }

        book.Quantity -= bookSale.Quantity;

        await BookValidator.ValidateAndThrowAsync(book);

        await BookRepository.UpdateAsync();
    }

}
