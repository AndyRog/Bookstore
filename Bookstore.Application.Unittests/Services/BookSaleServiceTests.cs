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
        private IMapper Mapper { get; }
        private BookSaleValidator BookSaleValidator { get; }
        private BookValidator BookValidator { get; }

        public BookSaleServiceTests()
        {
            Mapper = new MapperConfiguration(cfg =>cfg.AddMaps(typeof(DtoEntityMapperProfile))).CreateMapper();
           
            BookSaleValidator = new BookSaleValidator();
            
            BookValidator = new BookValidator();
                       
        }

        [Fact]
        public async Task Quantaty_Updated()
        {
            //Arrenge
            var bookSale = new BookSale(1, 1);
            
            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).ReturnsAsync(new Book() { Quantity = 1, Author = new Author()});
            var bookSaleService = new BookSaleService( bookRepositoryMock.Object, BookSaleValidator, BookValidator);
            //Act
            await bookSaleService.ProcessBookDelivery(bookSale);

            //Assert
            bookRepositoryMock.Verify(mock => mock.UpdateAsync(), Times.Once());
        }

        [Fact]
        public void BookNotFoundException_For_Non_Existent_BookId()
        {
            //Arrenge
            var bookSale = new BookSale(1, 1);

            var bookRepositoryMock = new Mock<IBookRepository>();
            bookRepositoryMock.Setup(mock => mock.GetBookByIdAsync(1)).Returns<Book?>(null);
            var bookSaleService = new BookSaleService(bookRepositoryMock.Object, BookSaleValidator, BookValidator);
            //Act
            Func<Task> func = async () => await bookSaleService.ProcessBookDelivery(bookSale);

            //Assert
           Assert.ThrowsAsync<BookNotFoundException>(func);

        }
    }
}
