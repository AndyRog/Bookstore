using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Services;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services
{
    public class AuthorUpdateServicesTests
    {
        private AuthorUpdateValidator Validator { get; }
        private IMapper Mapper { get; }

        public AuthorUpdateServicesTests()
        {
            Mapper = new MapperConfiguration(config => config.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();

            Validator = new AuthorUpdateValidator();
        }

        [Fact]
        public async Task Author_Updated()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1,"Test", "Test");
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1)).ReturnsAsync(new Author());
            var authorUpdateService = new AuthorUpdateService(authorRepositoryMock.Object, Mapper, Validator);

            //Act
            await authorUpdateService.UpdateAuthorAsync(authorUpdate);

            //Assert
            authorRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once);

        }

        [Fact]
        public void AuthorNotFoundException_For_Non_Existent_Id()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1, "Test", "Test");
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            authorRepositoryMock.Setup(mock => mock.GetAuthorByIdAsync(1)).Returns<Author?>(null);
            var authorUpdateService = new AuthorUpdateService(authorRepositoryMock.Object, Mapper, Validator);

            //Actio
            Func<Task> func = async () => await authorUpdateService.UpdateAuthorAsync(authorUpdate);

            //Assert
            Assert.ThrowsAsync<AuthorNotFoundException>(func);
        }
    }

    
}
