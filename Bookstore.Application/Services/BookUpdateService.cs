using AutoMapper;
using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Unittests.Services;

public  class BookUpdateService
{
    //Property
    public IBookRepository BookRepository { get; }
    public IAuthorRepository AuthorRepository { get; }
    public IMapper Mapper { get; }
    public BookValidator BookValidator { get; }
    public BookUpdateValidator BookUpdateValidator { get; }
    public IBookRepository Object1 { get; }
    public IAuthorRepository Object2 { get; }
    public BookUpdateValidator BookUpdateValidator1 { get; }
    public BookValidator BookValidator1 { get; }

    //Construct
    public BookUpdateService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, BookValidator bookValidator, BookUpdateValidator bookUpdateValidator)
    {
        BookRepository = bookRepository;
        AuthorRepository = authorRepository;
        Mapper = mapper;
        BookValidator = bookValidator;
        BookUpdateValidator = bookUpdateValidator;
    }

    public BookUpdateService(IBookRepository object1, IAuthorRepository object2, IMapper mapper, BookUpdateValidator bookUpdateValidator, BookValidator bookValidator)
    {
        Object1 = object1;
        Object2 = object2;
        Mapper = mapper;
        BookUpdateValidator1 = bookUpdateValidator;
        BookValidator1 = bookValidator;
    }

    // Method
    public async Task UpdateBookAsync(BookUpdate bookUpdate)
    {
        await BookUpdateValidator.ValidateAsync(bookUpdate);

        Book? book = await BookRepository.GetBookByIdAsync(bookUpdate.BookId);

        if (book == null)
        {
            throw new BookNotFoundException();
        }

        Author? author = await AuthorRepository.GetAuthorByIdAsync(bookUpdate.AuthorId);
        if (author == null) 
        {
            throw new AuthorNotFoundException();
        }


        Book? existingBookForIsbn = await BookRepository.GetBookByIsbnAsync(bookUpdate.ISBN);
        if(existingBookForIsbn != null && existingBookForIsbn.Id != book.Id)
        {
            throw new IsbnDublicateException();
        }

        book.Author = author;
        Mapper.Map(bookUpdate, book);
        await BookValidator.ValidateAndThrowAsync(book);
        await BookRepository.UpdateAsync();

    }
   
}
