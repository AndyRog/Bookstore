using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Contracts
{
    public interface IApplicationLogger<T>
    {
        //Author
        void LogCreateAuthorAsyncCalled(AuthorCreate authorCreate);
        void LogValidationErrorForCreateAuthor(ValidationException ex, AuthorCreate authorCreate);
        void LogAuthorCreated(long id);
        void LogUpdateAuthorAsyncCalled(AuthorUpdate authorUpdate);
        void LogValidationErrorForAuthorUpdate(ValidationException ex, AuthorUpdate authorUpdate);
        void LogAuthorNotFound(long authorId);
        void LogAuthorUpdated(Author author);
      
        //Book
        void LogCreateBookAsyncCalled(BookCreate bookCreate);
        void LogValidationErrorForBookCreate(ValidationException ex, BookCreate bookCreate);
        void LogIsbnDuplicate(string isbn);
        void LogBookCreated(long id);
        void LogProcessBookDeliveryAsyncCalled(BookDelivery bookDelivery);
        void LogValidationErrorForBook(ValidationException ex, Book book);
        void LogUpdateBookCalled(BookUpdate bookUpdate);
        void LogProcessBookSaleAsyncCalled(BookSale bookSale);
        void LogValidationErrorForBookDelivery(ValidationException ex,BookDelivery bookDelivery);
        void LogValidationErrorForBookUpdate(ValidationException ex,BookUpdate bookUpdate);        
        void LogBookNotFound(long bookId);
        void LogValidationErrorForBookSale(ValidationException ex, BookSale bookSale);
        void LogBookUpdated(Book book);
        void LogBookSaleProcessed(long id, int quantity);
        void LogBookDeliveryProcessed(long id, int quantity);
        

       
        
       

    }
}
