using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application
{
    public class ApplicationLogger<T> : IApplicationLogger<T>
    {
        public ILogger<T> Logger { get; }
       
        public ApplicationLogger(ILogger<T> logger )
        {
            Logger = logger;
        }

        public void LogCreateAuthorAsyncCalled(AuthorCreate authorCreate)
        {
            Logger.LogInformation("AuthorCreate called. {authorCreate}", authorCreate);
        }

        public void LogAuthorCreated(long id)
        {
            Logger.LogInformation($"AuthorCreated: {id}");
        }

        public void LogValidationErrorForCreateAuthor(ValidationException ex, AuthorCreate authorCreate)
        {
            Logger.LogError(ex, "Validation Error for CreateAuthor. {authorCreate}", authorCreate);

        }

        public void LogAuthorNotFound(long authorId)
        {
           Logger.LogError($"Author not found. {authorId}");
        }

        public void LogAuthorUpdated(Author author)
        {
            Logger.LogInformation($"Author updated: {author}");
        }

        public void LogUpdateAuthorAsyncCalled(AuthorUpdate authorUpdate)
        {
            Logger.LogInformation("AuthorUpdate called. {authorUpdate}", authorUpdate);
        }

        public void LogValidationErrorForAuthorUpdate(ValidationException ex, AuthorUpdate authorUpdate)
        {
            Logger.LogError(ex, "Validation Error for AuthorUpdate. {authorUpdate}", authorUpdate);
        }

        public void LogCreateBookAsyncCalled(BookCreate bookCreate)
        {
            Logger.LogInformation($"CreateBook called. {bookCreate}");
        }

        public void LogValidationErrorForBookCreate(ValidationException ex, BookCreate bookCreate)
        {
            Logger.LogError(ex, $"Validation Error for BookCreate. {bookCreate}");
        }

        public void LogIsbnDuplicate(string isbn)
        {
          Logger.LogInformation($"ISBN duplicate{isbn}");
        }

        public void LogBookCreated(long id)
        {
            Logger.LogInformation($"Book created. {id}");
        }

        public void LogValidationErrorForBook(ValidationException ex, Book book)
        {
            Logger.LogError(ex,$"Validation Error for book. {book}");
           
        }

        public void LogProcessBookDeliveryAsyncCalled(BookDelivery bookDelivery)
        {
            Logger.LogInformation($"ProcessBookDelivery called. {bookDelivery}");
        }

        public void LogUpdateBookCalled(BookUpdate bookUpdate)
        {
            Logger.LogInformation($"UpdateBook called. {bookUpdate}");
        }

        public void LogProcessBookSaleAsyncCalled(BookSale bookSale)
        {
            Logger.LogInformation($"ProcessBookSale called. {bookSale}");
        }

        public void LogValidationErrorForBookDelivery(ValidationException ex, BookDelivery bookDelivery)
        {
            Logger.LogError(ex, $"Validation Error for BookDelivery. {bookDelivery}");
        }

        public void LogValidationErrorForBookUpdate(ValidationException ex, BookUpdate bookUpdate)
        {
            Logger.LogError(ex, $"Validation Error for BookUpdate. {bookUpdate}");
        }

        public void LogBookNotFound(long bookId)
        {
           Logger.LogError($"Book not found. {bookId}");
        }

        public void LogValidationErrorForBookSale(ValidationException ex, BookSale bookSale)
        {
            Logger.LogError(ex, $"Validation Error for BookSale {bookSale}");
        }

        public void LogBookUpdated(Book book)
        {
            Logger.LogInformation($"Book updated. {book}");
        }

        public void LogBookSaleProcessed(long id, int quantity)
        {
            Logger.LogInformation($"BookSale processed. {id} {quantity}");
        }

        public void LogBookDeliveryProcessed(long id, int quantity)
        {
            Logger.LogInformation($"BookDelivery processed. {id} {quantity}");
        }
    }
}
