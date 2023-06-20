using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Services;

public class BookDeliveryService
{
    public IBookRepository BookRepository { get; }
    public BookDeliveryValidator BookDeliveryValidator { get; }
    public IApplicationLogger<BookDeliveryService> Logger { get; }

    public BookDeliveryService(IBookRepository bookRepository, BookDeliveryValidator bookDeliveryValidator, IApplicationLogger<BookDeliveryService> logger)
    {
        BookRepository = bookRepository;
        BookDeliveryValidator = bookDeliveryValidator;
        Logger = logger;
    }
   
    
    public async Task ProcessBookDeliveryAsync(BookDelivery bookDelivery)
    {
       Logger.LogProcessBookDeliveryAsyncCalled(bookDelivery);
        try
        {
            await BookDeliveryValidator.ValidateAndThrowAsync(bookDelivery);
        }
        catch (ValidationException ex)
        {
            Logger.LogValidationErrorForBookDelivery(ex, bookDelivery);         
            throw;
        }
        

        Book? book = await BookRepository.GetBookByIdAsync(bookDelivery.BookId);
        if (book == null)
        {
            Logger.LogBookNotFound(bookDelivery.BookId);
            throw new BookNotFoundException();
        }

        book.Quantity += bookDelivery.Quantity;

        await BookRepository.UpdateAsync();

        Logger.LogBookDeliveryProcessed(book.Id, bookDelivery.Quantity);
    }

}
