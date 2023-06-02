using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
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

        public BookCreateService(IBookRepository bookRepository, IAuthorRepository authorRepository, IMapper mapper, BookCreateValidator bookCreateValidator, BookValidator bookValidator)
        {
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
            this.Mapper = mapper;
            BookCreateValidator = bookCreateValidator;
            BookValidator = bookValidator;
        }
        public async Task<long> CreateBook(BookCreate bookCreate)
        {
            await BookCreateValidator.ValidateAndThrowAsync(bookCreate);
            var book = Mapper.Map<Book>(bookCreate);

            Author? author = await AuthorRepository.FindByAsync(bookCreate.AuthorId);
            Book? existingBookForIsbn = await BookRepository.GetBookIsbn(bookCreate.ISBN);
        }
    }
}
