using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Services;
using Bookstore.Application.Validation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services
{
    public class AutherCreateServiceTest
    {
        private IMapper Mapper { get; }
        private AuthorCreateValidator Validator { get; }

        public AutherCreateServiceTest()
        {
            Mapper = new MapperConfiguration(config => config.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();

            Validator = new AuthorCreateValidator();
        }

        [Fact]
        public async Task Create_Author()
        {
            //Arrange
            var authorCreate = new AuthorCreate("test", "test");

            var authorRepositoryMock = new Mock<IAuthorRepository>();

            var authorCreateService = new AuthorCreateService(authorRepositoryMock.Object, Mapper, Validator);
        }

    }
}
