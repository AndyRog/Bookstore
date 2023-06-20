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
    public IApplicationLogger<BookSaleService> Logger { get; }

    public BookSaleService(IBookRepository bookRepository, BookSaleValidator bookSaleValidator, BookValidator bookValidator, IApplicationLogger<BookSaleService> logger)
    {
        BookRepository = bookRepository;
        BookSaleValidator = bookSaleValidator;
        BookValidator = bookValidator;
        Logger = logger;
    }
   
    
    public async Task ProcessBookSaleAsync(BookSale bookSale)
    {
        Logger.LogProcessBookSaleAsyncCalled(bookSale);
        try
        {
          await BookSaleValidator.ValidateAndThrowAsync(bookSale);
        }
        catch (ValidationException ex)
        {
            Logger.LogValidationErrorForBookSale(ex, bookSale);
            throw;
        }
        
        Book? book = await BookRepository.GetBookByIdAsync(bookSale.BookId);

        if (book == null)
        {
            Logger.LogBookNotFound(bookSale.BookId);
            throw new BookNotFoundException();
        }

        book.Quantity -= bookSale.Quantity;
        try
        {
            await BookValidator.ValidateAndThrowAsync(book);

        }
        catch (ValidationException ex)
        {
            Logger.LogValidationErrorForBook(ex, book);
            throw;
        }


        await BookRepository.UpdateAsync();
        Logger.LogBookSaleProcessed(book.Id,bookSale.Quantity);
    }

}
