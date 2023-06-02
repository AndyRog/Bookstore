using AutoMapper;
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
        public async Task Add_Book()
        {
            //Arrange
            var bookCreate = new BookCreate("1234567891234", "Test",1,0);
            var bookRepositoryMock = new Mock<IBookRepository>();
            var authorRepositoryMock = new Mock<IAuthorRepository>();

            authorRepositoryMock.Setup(mock => mock.GetAuthorById(1)).ReturnsAsync(new Author());
            var bookCreateService = new BookCreateService(bookRepositoryMock.Object, authorRepositoryMock.Object, Mapper,BookCreateValidator,BookValidator);

            //Act
            await bookCreateService.CreateBook(bookCreate);

            //Assert
            bookRepositoryMock.Verify(mock => mock.AddBook(It.IsAny<Book>()), Times.Once);

        }
    }
}
