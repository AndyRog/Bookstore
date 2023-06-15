using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
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

        public void LogValidationErrorInCreateAuthor(ValidationException ex, AuthorCreate authorCreate)
        {
            Logger.LogError(ex, "Validation Error in CreateAuthor. {authorCreate}", authorCreate);

        }
    }
}
