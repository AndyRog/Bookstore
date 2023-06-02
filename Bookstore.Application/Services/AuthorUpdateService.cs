using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
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
        public AuthorUpdateService(IAuthorRepository authorRepository, IMapper mapper, AuthorUpdateValidator authorUpdateValidator)
        {
            AuthorRepository = authorRepository;
            Mapper = mapper;
            AuthorUpdateValidator = authorUpdateValidator;
        }

        public async Task UpdateAuthor(AuthorUpdate authorUpdate)
        {
            await Validation.AuthorUpdateValidator.ValidateAndThrowAsync(authorUpdate);
        }
       
    }
}
