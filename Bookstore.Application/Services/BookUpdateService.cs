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

    //Construct
    public BookUpdateService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, BookValidator bookValidator, BookUpdateValidator bookUpdateValidator)
    {
        BookRepository = bookRepository;
        AuthorRepository = authorRepository;
        Mapper = mapper;
        BookValidator = bookValidator;
        BookUpdateValidator = bookUpdateValidator;
    }

    // Method
    public async Task UpdateBook(BookUpdate bookUpdate)
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
            throw new BookForIsbnDublicateException();
        }

        book.Author = author;
        Mapper.Map(bookUpdate, book);
        await BookValidator.ValidateAndThrowAsync(book);
        await BookRepository.UpdateAsync();

    }
   
}
