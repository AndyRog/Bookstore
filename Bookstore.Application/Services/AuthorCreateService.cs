using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Services;

public class AuthorCreateService
{
    public IAuthorRepository AuthorRepository { get; }
    
    public AuthorCreateService(IAuthorRepository authorRepository, IMapper mapper, AuthorCreateValidator authorCreateValidator)
    {
        AuthorRepository = authorRepository;
    }
    public async Task<long> CreateAuthor(AuthorCreate authorCreate)
    {

    }
}
