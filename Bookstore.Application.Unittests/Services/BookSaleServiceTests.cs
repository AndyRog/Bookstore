using AutoMapper;
using Bookstore.Applicatio.Exceptions;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
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
    public class BookSaleServiceTests
    {
        
        private BookSaleValidator BookSaleValidator { get; }
        private BookValidator BookValidator { get; }

        public BookSaleServiceTests()
        {
           BookSaleValidator = new BookSaleValidator();
            
            BookValidator = new BookValidator();
                       
        }

        [Fact]
        public async Task Quantity_Decreased()
        {
            //Arrange
            var bookSale = new BookSale(1, 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var book = new Book() { Quantity = 1, Author = new Author() };
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(book);
            var loggerMock = new Mock<IApplicationLogger<BookSaleService>>();
            var bookSaleService = new BookSaleService(bookRepositoryMock.Object,
                BookSaleValidator, BookValidator, loggerMock.Object);

            //Act
            await bookSaleService.ProcessBookSaleAsync(bookSale);

            //Assert
            Assert.Equal(0, book.Quantity);
            bookRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once);
            loggerMock.Verify(mock => mock.LogProcessBookSaleAsyncCalled(bookSale));
            loggerMock.Verify(mock => mock.LogBookSaleProcessed(It.IsAny<long>(), bookSale.Quantity));
        }

        [Fact]
        public void BookNotFoundException_For_Non_Existent_BookId()
        {
            //Arrange
            var bookSale = new BookSale(1, 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var loggerMock = new Mock<IApplicationLogger<BookSaleService>>();
            var bookSaleService = new BookSaleService(bookRepositoryMock.Object,
                BookSaleValidator, BookValidator, loggerMock.Object);

            //Act
            Func<Task> func = async () => await
            bookSaleService.ProcessBookSaleAsync(bookSale);

            //Assert
            Assert.ThrowsAsync<BookNotFoundException>(func);
            loggerMock.Verify(mock => mock.LogProcessBookSaleAsyncCalled(bookSale));
            loggerMock.Verify(mock => mock.LogBookNotFound(bookSale.BookId));
        }

        [Fact]
        public void ValidationError_For_Invalid_BookSale()
        {
            //Arrange
            var bookSale = new BookSale(1, -1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var book = new Book() { Quantity = 1, Author = new Author() };
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(book);
            var loggerMock = new Mock<IApplicationLogger<BookSaleService>>();
            var bookSaleService = new BookSaleService(bookRepositoryMock.Object,
                BookSaleValidator, BookValidator, loggerMock.Object);

            //Act
            Func<Task> func = async () => await
            bookSaleService.ProcessBookSaleAsync(bookSale);

            //Assert
            Assert.ThrowsAsync<FluentValidation.ValidationException>(func);
            loggerMock.Verify(mock => mock.LogProcessBookSaleAsyncCalled(bookSale));
            loggerMock.Verify(mock => mock.LogValidationErrorForBookSale(It.IsAny<FluentValidation.ValidationException>(),
                bookSale));
        }

        [Fact]
        public void ValidationError_For_Invalid_Book()
        {
            //Arrange
            var bookSale = new BookSale(1, 1);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var book = new Book() { Quantity = 0, Author = new Author() };
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1))
                .ReturnsAsync(book);
            var loggerMock = new Mock<IApplicationLogger<BookSaleService>>();
            var bookSaleService = new BookSaleService(bookRepositoryMock.Object,
                BookSaleValidator, BookValidator, loggerMock.Object);

            //Act
            Func<Task> func = async () => await
            bookSaleService.ProcessBookSaleAsync(bookSale);

            //Assert
            Assert.ThrowsAsync<FluentValidation.ValidationException>(func);
            loggerMock.Verify(mock => mock.LogProcessBookSaleAsyncCalled(bookSale));
            loggerMock.Verify(mock => mock.LogValidationErrorForBook(It.IsAny<FluentValidation.ValidationException>(),
                It.IsAny<Book>()));
        }
    }
}
