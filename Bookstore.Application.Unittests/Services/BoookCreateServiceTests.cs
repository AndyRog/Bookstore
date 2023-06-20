using AutoMapper;
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services;

public class BoookCreateServiceTests
{
    public IMapper Mapper { get; }
    public BookCreateValidator BookCreateValidator { get; }
    public BookValidator BookValidator{ get; }


    public BoookCreateServiceTests() 
    {
        Mapper = new MapperConfiguration(cfg => cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();

        BookCreateValidator = new BookCreateValidator();
        BookValidator = new BookValidator();
    }

    [Fact]
    public async Task Book_Added()
    {
        //Arrange
        var bookCreate = new BookCreate("1234567891234", "Test", 1, 0);
        var bookRepositoryMock = new Mock<IBookRepository>();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
            .ReturnsAsync(new Author());
        var applicationLoggerMock = new Mock<IApplicationLogger<BookCreateService>>();
        var bookCreateService = new BookCreateService(bookRepositoryMock.Object,
            authorRepositoryMock.Object, Mapper, BookCreateValidator,
            BookValidator, applicationLoggerMock.Object);

        //Act
        await bookCreateService.CreateBookAsync(bookCreate);

        //Assert
        bookRepositoryMock.Verify(mock => mock.AddBookAsync(It.IsAny<Book>()),
           Times.Once);
        applicationLoggerMock.Verify(mock => mock.LogCreateBookAsyncCalled(bookCreate), Times.Once);
        applicationLoggerMock.Verify(mock => mock.LogBookCreated(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void AuthorNotFoundException_For_Non_Existent_Author()
    {
        //Arrange
        var bookCreate = new BookCreate("1234567891234", "Test", 1, 0);
        var bookRepositoryMock = new Mock<IBookRepository>();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        var applicationLoggerMock = new Mock<IApplicationLogger<BookCreateService>>();
        var bookCreateService = new BookCreateService(bookRepositoryMock.Object,
            authorRepositoryMock.Object, Mapper, BookCreateValidator,
            BookValidator, applicationLoggerMock.Object);

        //Act
        Func<Task> func = async () => await bookCreateService
        .CreateBookAsync(bookCreate);

        //Assert
        Assert.ThrowsAsync<AuthorNotFoundException>(func);
        applicationLoggerMock.Verify(mock => mock.LogCreateBookAsyncCalled(bookCreate), Times.Once);
        applicationLoggerMock.Verify(mock => mock.LogAuthorNotFound(It.IsAny<long>()), Times.Once);
    }

    [Fact]
    public void IsbnDublicateException_For_Exicting_ISBN()
    {
        //Arrange
        var bookCreate = new BookCreate("1234567891234", "Test", 1, 0);
        
        var bookRepositoryMock = new Mock<IBookRepository>();
        bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).ReturnsAsync(new Book() { Id = 1}); bookRepositoryMock.Setup(mock => mock.GetBookByIsbnAsync("1234567891234")).ReturnsAsync(new Book() { Id = 2});
        
        var applicationLoggerMock = new Mock<IApplicationLogger<BookCreateService>>();

        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1)).ReturnsAsync(  new Author());

        var bookCreateService = new BookCreateService(bookRepositoryMock.Object, authorRepositoryMock.Object, Mapper, BookCreateValidator, BookValidator, applicationLoggerMock.Object);

        //Act
        Func<Task> func = async () => await bookCreateService.CreateBookAsync(bookCreate);

        //Assert
        Assert.ThrowsAsync<IsbnDublicateException>(func);
        applicationLoggerMock.Verify(mock => mock.LogCreateBookAsyncCalled(bookCreate));
        applicationLoggerMock.Verify(mock => mock.LogIsbnDuplicate(It.IsAny<string>()), Times.Once);

    }
    [Fact]
    public void ValidationError_For_Invalid_BookCreate()
    {
        //Arrange
        var bookCreate = new BookCreate("1234567891234", string.Empty, 1, 0);
        var bookRepositoryMock = new Mock<IBookRepository>();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
            .ReturnsAsync(new Author());
        var applicationLoggerMock = new Mock<IApplicationLogger<BookCreateService>>();
        var bookCreateService = new BookCreateService(bookRepositoryMock.Object,
            authorRepositoryMock.Object, Mapper, BookCreateValidator,
            BookValidator, applicationLoggerMock.Object);

        //Act
        Func<Task> func = async () => await bookCreateService
         .CreateBookAsync(bookCreate);

        //Assert
        Assert.ThrowsAsync<ValidationException>(func);
        applicationLoggerMock.Verify(mock => mock.LogCreateBookAsyncCalled(bookCreate), Times.Once);
        applicationLoggerMock.Verify(mock =>
        mock.LogValidationErrorForBookCreate(It.IsAny<FluentValidation.ValidationException>(), bookCreate), Times.Once);
    }

    [Fact]
    public void ValidationError_For_Invalid_Book()
    {
        //Arrange
        var bookCreate = new BookCreate("12345674", "Test", 1, 0);
        var bookRepositoryMock = new Mock<IBookRepository>();
        var authorRepositoryMock = new Mock<IAuthorRepository>();
        authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1))
            .ReturnsAsync(new Author());
        var applicationLoggerMock = new Mock<IApplicationLogger<BookCreateService>>();
        var bookCreateService = new BookCreateService(bookRepositoryMock.Object,
            authorRepositoryMock.Object, Mapper, BookCreateValidator,
            BookValidator, applicationLoggerMock.Object);

        //Act
        Func<Task> func = async () => await bookCreateService
         .CreateBookAsync(bookCreate);

        //Assert
        Assert.ThrowsAsync<ValidationException>(func);
        applicationLoggerMock.Verify(mock => mock.LogCreateBookAsyncCalled(bookCreate), Times.Once);
        applicationLoggerMock.Verify(mock =>
        mock.LogValidationErrorForBook(It.IsAny<FluentValidation.ValidationException>(), It.IsAny<Book>()),
        Times.Once);
    }
}
