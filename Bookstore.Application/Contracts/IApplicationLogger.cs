using Bookstore.Application.Dtos;
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
        void LogAuthorCreated(long id);
        void LogCreateAuthorAsyncCalled(AuthorCreate authorCreate);
        void LogValidationErrorInCreateAuthor(ValidationException ex, AuthorCreate authorCreate);
    }
}
