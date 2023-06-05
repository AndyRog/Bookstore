using AutoMapper;
using Bookstore.Applicatio.Exceptions;
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
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).ReturnsAsync(new Book());
            var bookDeliveryService = new BookDeliveryService( bookRepositoryMock.Object, BookDeliveryValidator);
            //Act
            await bookDeliveryService.ProcessBookDeliveryAsync(bookDelivery);

            //Assert
            bookRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once());
        }

        [Fact]
        public void BookNotFoundException_For_Non_Existent_BookId()
        {
            //Arrenge
            var bookDelivery = new BookDelivery(1, 1);

            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).Returns<Book?>(null);
            var bookDeliveryService = new BookDeliveryService(bookRepositoryMock.Object, BookDeliveryValidator);
            //Act
            Func<Task> func = async () => await bookDeliveryService.ProcessBookDeliveryAsync(bookDelivery);

            //Assert
           Assert.ThrowsAsync<BookNotFoundException>(func);

        }
    }
}
