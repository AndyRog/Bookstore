using AutoMapper;
using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Bookstore.Domain.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services
{
    public class BookUpdateserviceTests
    {
        private IMapper Mapper { get; }
        private BookUpdateValidator BookUpdateValidator { get; }
        private BookValidator BookValidator { get; }

        public BookUpdateserviceTests()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();

            BookValidator = new BookValidator();
            BookUpdateValidator = new BookUpdateValidator();
        }

        [Fact]
        public async Task Update_Book()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);

            var bookRepositoryMock = new Mock<IBookRepository >();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).ReturnsAsync(new Book());

            var authorRepositoryMock = new Mock<IAuthorRepository >();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1)).ReturnsAsync(new Author());

            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object, authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator);

            //Act
            await bookUpdateService.UpdateBook(bookUpdate);

            //Assert
            bookRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once());

        }
        
        [Fact]
        public void BookNotFondException_For_Non_Existent_Book()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);

            var bookRepositoryMock = new Mock<IBookRepository >();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).Returns<Book>(null);

            var authorRepositoryMock = new Mock<IAuthorRepository >();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1)).ReturnsAsync(new Author());

            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object, authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator);

            //Act
            Func<Task> func =  async () => await bookUpdateService.UpdateBook(bookUpdate);

            //Assert
            Assert.ThrowsAsync<BookNotFoundException>(func);

        }
    }
}
