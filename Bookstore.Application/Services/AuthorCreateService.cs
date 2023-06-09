﻿using AutoMapper;
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

public class AuthorCreateService
{
    public IAuthorRepository AuthorRepository { get; }
    public IMapper Mapper { get; }
    public AuthorCreateValidator AuthorCreateValidator { get; }

    public AuthorCreateService(IAuthorRepository authorRepository, IMapper mapper, AuthorCreateValidator authorCreateValidator)
    {
        AuthorRepository = authorRepository;
        Mapper = mapper;
        AuthorCreateValidator = authorCreateValidator;
    }
    public async Task<long> CreateAuthorAsync(AuthorCreate authorCreate)
    {
        await AuthorCreateValidator.ValidateAndThrowAsync(authorCreate);
        var author = Mapper.Map<Author>(authorCreate);
        long id = await AuthorRepository.AddAuthorAsync(author);
        return id;
    }
}
