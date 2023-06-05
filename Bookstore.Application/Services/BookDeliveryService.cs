using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using FluentValidation;
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

    public BookDeliveryService(IBookRepository bookRepository, BookDeliveryValidator bookDeliveryValidator)
    {
        BookRepository = bookRepository;
        BookDeliveryValidator = bookDeliveryValidator;
    }
   
    
    public async Task ProcessBookDelivery(BookDelivery bookDelivery)
    {
        await BookDeliveryValidator.ValidateAndThrowAsync(bookDelivery);

        Book? book = await BookRepository.GetBookByIdAsync(bookDelivery.BookId);
        if (book == null)
        {
            throw new BookNotFoundException();
        }

        book.Quantity += bookDelivery.Quantity;

        await BookRepository.UpdateAsync();
    }

}
