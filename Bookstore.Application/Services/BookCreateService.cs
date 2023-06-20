using AutoMapper;
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

namespace Bookstore.Application.Services
{
    public class BookCreateService
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        public IMapper Mapper { get; }
        public BookCreateValidator BookCreateValidator { get; }
        public BookValidator BookValidator { get; }
        public IApplicationLogger<BookCreateService> Logger { get; }

        public BookCreateService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, BookCreateValidator bookCreateValidator, BookValidator bookValidator, IApplicationLogger<BookCreateService> logger)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            this.Mapper = mapper;
            BookCreateValidator = bookCreateValidator;
            BookValidator = bookValidator;
            Logger = logger;
        }


        public async Task<long> CreateBookAsync(BookCreate bookCreate)
        {
            Logger.LogCreateBookAsyncCalled(bookCreate);
            try
            {
                await BookCreateValidator.ValidateAndThrowAsync(bookCreate);
            }
            catch (ValidationException ex)
            {
                Logger.LogValidationErrorForBookCreate(ex, bookCreate);
                throw;
            }
            
            var book = Mapper.Map<Book>(bookCreate);

            Author? author = await AuthorRepository.GetAuthorByIdAsync(bookCreate.AuthorId);

            Book? existingBookForIsbn = await BookRepository.GetBookByIsbnAsync(bookCreate.ISBN);

            if(author == null)
            {
                Logger.LogAuthorNotFound(bookCreate.AuthorId);
                throw new AuthorNotFoundException();
            }
            if(existingBookForIsbn != null) 
            {
                Logger.LogIsbnDuplicate(bookCreate.ISBN);
                throw new IsbnDuplicateException();
            }

            book.Author = author;
            try
            {
                await BookValidator.ValidateAndThrowAsync(book);
            }
            catch (ValidationException ex)
            {
                Logger.LogValidationErrorForBook(ex, book);
                throw;
            }
            
            var id = await BookRepository.AddBookAsync(book);
            Logger.LogBookCreated(id);
            return id;
        }
    }
}
