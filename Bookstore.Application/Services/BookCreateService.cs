using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Validation;
using Bookstore.Domain.Validation;
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
        public IMapper Mapper { get; }
        public BookCreateValidator BookCreateValidator { get; }
        public BookValidator BookValidator { get; }

        public BookCreateService(IBookRepository bookRepository, IMapper mapper, BookCreateValidator bookCreateValidator, BookValidator bookValidator)
        {
            BookRepository = bookRepository;
            this.Mapper = mapper;
            BookCreateValidator = bookCreateValidator;
            BookValidator = bookValidator;
        }

    }
}
