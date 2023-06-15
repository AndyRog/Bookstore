using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Services
{
    public class AuthorUpdateService
    {
        public IAuthorRepository AuthorRepository { get; }
        public IMapper Mapper { get; }
        public AuthorUpdateValidator AuthorUpdateValidator { get; }
        public IApplicationLogger<AuthorUpdateService> Logger { get; }

        public AuthorUpdateService(IAuthorRepository authorRepository, IMapper mapper, AuthorUpdateValidator authorUpdateValidator, IApplicationLogger<AuthorUpdateService> logger)
        {
            AuthorRepository = authorRepository;
            Mapper = mapper;
            AuthorUpdateValidator = authorUpdateValidator;
            Logger = logger;
        }

        public async Task UpdateAuthorAsync(AuthorUpdate authorUpdate)
        {
            Logger.LogUpdateAuthorAsyncCalled(authorUpdate);
            try
            {
                await AuthorUpdateValidator.ValidateAndThrowAsync(authorUpdate);
            }
            catch (ValidationException ex)
            {
                Logger.LogValidationErrorInUpdateAauthor(ex, authorUpdate);
                throw;
            }
           
            Author? author = await AuthorRepository.GetAuthorByIdAsync(authorUpdate.AuthorId);

            if (author == null)
            {
                Logger.LogAuthorNotFound(authorUpdate.AuthorId);
                throw new AuthorNotFoundException();
            }

            Mapper.Map(authorUpdate, author);
            await AuthorRepository.UpdateAsync();

            Logger.LogAuthorUpdated(author);

        }
       
    }
}
