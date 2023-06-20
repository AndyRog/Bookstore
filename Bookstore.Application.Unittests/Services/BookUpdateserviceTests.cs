using AutoMapper;
using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Services;
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
        public async Task Book_Updated()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(new Book());
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
                .ReturnsAsync(new Author());
            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();
            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            bookRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogBookUpdated(It.IsAny<Book>()));
        }

        [Fact]
        public void BookNotFoundException_For_Non_Existent_Book()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
                .ReturnsAsync(new Author());
            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();
            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            Func<Task> func = async () =>
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            Assert.ThrowsAsync<BookNotFoundException>(func);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogBookNotFound(bookUpdate.BookId));
        }

        [Fact]
        public void AuthorNotFoundException_For_Non_Existent_Author()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(new Book());
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();

            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            Func<Task> func = async () =>
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            Assert.ThrowsAsync<AuthorNotFoundException>(func);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogAuthorNotFound(bookUpdate.AuthorId));
        }

        [Fact]
        public void IsbnDuplicateException_For_Duplicate_Isbn()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", "Test", 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(new Book() { Id = 1 });
            bookRepositoryMock.Setup(mock => mock.GetBookByIsbnAsync("1234567891234"))
                .ReturnsAsync(new Book() { Id = 2 });
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
                .ReturnsAsync(new Author());
            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();
            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            Func<Task> func = async () =>
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            Assert.ThrowsAsync<IsbnDuplicateException>(func);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogIsbnDuplicate(bookUpdate.ISBN));
        }

        [Fact]
        public void ValidationException_For_Invalid_BookUpdate()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "1234567891234", string.Empty, 1);

            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(new Book() { Id = 1 });

            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
                .ReturnsAsync(new Author());

            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();

            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            Func<Task> func = async () =>
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            Assert.ThrowsAsync<IsbnDuplicateException>(func);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogValidationErrorForBookUpdate(
                It.IsAny<FluentValidation.ValidationException>(), bookUpdate));
        }

        [Fact]
        public void ValidationException_For_Invalid_Book()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1, "12345678", "Test", 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(new Book() { Id = 1 });
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
                .ReturnsAsync(new Author());
            var loggerMock = new Mock<IApplicationLogger<BookUpdateService>>();
            var bookUpdateService = new BookUpdateService(bookRepositoryMock.Object,
                authorRepositoryMock.Object, Mapper, BookValidator, BookUpdateValidator,
                loggerMock.Object);

            //Act
            Func<Task> func = async () =>
            await bookUpdateService.UpdateBookAsync(bookUpdate);

            //Assert
            Assert.ThrowsAsync<IsbnDuplicateException>(func);
            loggerMock.Verify(mock => mock.LogUpdateBookCalled(bookUpdate));
            loggerMock.Verify(mock => mock.LogValidationErrorForBook(
                It.IsAny<FluentValidation.ValidationException>(), It.IsAny<Book>()));
        }
    }
}
