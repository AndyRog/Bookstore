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
        void LogAuthorNotFound(long authorId);
        void LogAuthorCreated(long id);
        void LogAuthorUpdated(Author author);
        void LogCreateAuthorAsyncCalled(AuthorCreate authorCreate);
        void LogUpdateAuthorAsyncCalled(AuthorUpdate authorUpdate);
        void LogValidationErrorInCreateAuthor(ValidationException ex, AuthorCreate authorCreate);
        void LogValidationErrorInUpdateAuthor(ValidationException ex, AuthorUpdate authorUpdate);
    }
}
