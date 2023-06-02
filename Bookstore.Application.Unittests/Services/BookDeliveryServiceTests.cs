using AutoMapper;
using Bookstore.Application.Contracts;
using Bookstore.Application.Dtos;
using Bookstore.Application.Services;
using Bookstore.Application.Validation;
using Bookstore.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Services
{
    public class BookDeliveryServiceTests
    {
        private IMapper Mapper { get; }
        private BookDeliveryValidator BookDeliveryValidator { get; }

        public BookDeliveryServiceTests()
        {
            Mapper = new MapperConfiguration(cfg =>cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();
            BookDeliveryValidator = new BookDeliveryValidator();

            
        }

        [Fact]
        public async Task Quantaty_Updated()
        {
            //Arrenge
            var bookDelivery = new BookDelivery(1, 1);
            
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookById(1)).ReturnsAsync(new Book());
            var bookDeliveryService = new BookDeliveryService( bookRepositoryMock.Object, BookDeliveryValidator);
            //Act
            await bookDeliveryService.ProcessBookDelivery(bookDelivery);

            //Assert
            bookRepositoryMock.Verify(mock => mock.Update(), Times.Once());
        }

    }
}
