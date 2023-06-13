using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using BookStore.IntegrationTests.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTests
{
    public class BookSaleTests : IntegrationTestsBase, IDisposable
    {
        public Author Author { get; }
        public Book Book { get; }

        public BookSaleTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
            Author = new Author()
            {
                FirstName = "Test",
                LastName = "Test"
            };

            DbContext.Authors.Add(Author);
            DbContext.SaveChanges();

            Book = new Book() 
            {
                AuthorId = Author.Id,
                Title = "Test",
                Isbn = "1234567891234",
                Quantity = 1
            };

            DbContext.Books.Add(Book);
            DbContext.SaveChanges();

        }

        [Fact]
        public async Task Success_StatusCode_For_Valid_BookSale()
        {
            //Arrange
           var bookSale = new BookSale(Book.Id, 1);
               
            var bookSaleJson = JsonConvert.SerializeObject(bookSale);

            var content = new StringContent(bookSaleJson, Encoding.UTF8, "application/json");
            
            
          //Act
            var response = await Client.PostAsync(requestUri: "/Book/Sale", content);

            //Refresh Book from DB
          DbContext.Entry(Book).Reload();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(0, Book.Quantity);
        }
        
        [Fact]
        public async Task StatusCode_400_For_ValidationError()
        {
            //Arrange
            var bookSale = new BookSale(int.MaxValue, -1);

            var bookSaleJson = JsonConvert.SerializeObject(bookSale);

            var content = new StringContent(bookSaleJson, Encoding.UTF8, "application/json");


            //Act
            var response = await Client.PostAsync(requestUri: "/Book/Sale", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Validation Error", responseContent);
        }

        [Fact]
        public async Task StatusCode_400_For_Non_Existent_Book()
        {
            //Arrange
            var bookSale = new BookSale(int.MaxValue, 1);

            var bookSaleJson = JsonConvert.SerializeObject(bookSale);

            var content = new StringContent(bookSaleJson, Encoding.UTF8, "application/json");


            //Act
            var response = await Client.PostAsync(requestUri: "/Book/Sale", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Book not found", responseContent);
        }

        public void Dispose()
        {
            DbContext.Authors.Remove(Author);
            DbContext.SaveChanges();
            DbContext.Dispose();
        }


      
    }
}