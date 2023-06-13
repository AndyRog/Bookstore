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
    public class BookUpdateTests : IntegrationTestsBase, IDisposable
    {
        public Author Author { get; }
        public Book Book { get; }

        public BookUpdateTests(WebApplicationFactory<Startup> factory) : base(factory)
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
                Quantity = 0
            };

            DbContext.Books.Add(Book);
            DbContext.SaveChanges();

        }

        [Fact]
        public async Task Success_StatusCode_For_Updated_Book()
        {
            //Arrange
           var bookUpdate = new BookUpdate(Book.Id, Book.Isbn, "Title_1", Author.Id);
               
            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

          //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            //Refresh Book from DB
          DbContext.Entry(Book).Reload();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedBook, Book);
        }

        [Fact]
        public async Task StatusCode_400_For_Non_Existent_Author()
        {
            //Arrange
            var bookUpdate = new BookUpdate(Book.Id, Book.Isbn, "Title_1", int.MaxValue);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

            //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Author not found", responseContent);
        }
        
        [Fact]
        public async Task StatusCode_400_For_Non_Existent_Book()
        {
            //Arrange
            var bookUpdate = new BookUpdate(int.MaxValue, Book.Isbn, "Title_1", Author.Id);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

            //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Book not found", responseContent);
        }

        [Fact]
        public async Task StatusCode_400_For_Dublicate_ISBN()
        {
            //Arrange

            var book = new Book() 
            { 
                AuthorId = Author.Id,
                Title = "Title_2",
                Isbn ="1234567891239",
                Quantity = 0
            };

            DbContext.Books.Add(book);
            await DbContext.SaveChangesAsync();

            var bookUpdate = new BookUpdate(Book.Id, book.Isbn, "Title_1", Author.Id);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

            //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
           Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Isbn already Exists", responseContent);

           // Teardown
            DbContext.Books.Remove(book);
            await DbContext.SaveChangesAsync();
        }

            var bookUpdate = new BookUpdate(Book.Id, book.Isbn, "Title_1", Author.Id);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

        [Fact]
        public async Task StatusCode_400_For_ValidationError()
        {
            //Arrange
            var bookUpdate = new BookUpdate(Book.Id, "123", "Title_1", Author.Id);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

            //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Validation Error", responseContent);
        }

        [Fact]
        public async Task StatusCode_400_For_ValidationError()
        {
            //Arrange
            var bookUpdate = new BookUpdate(Book.Id, "123", "Title_1", Author.Id);

            var bookUpdateJson = JsonConvert.SerializeObject(bookUpdate);

            var content = new StringContent(bookUpdateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookUpdate);

            //Act
            var response = await Client.PutAsync(requestUri: "/Book/Update", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Contains("Validation Error", responseContent);
        }

        public void Dispose()
        {
            DbContext.Authors.Remove(Author);
            DbContext.SaveChanges();
            DbContext.Dispose();
        }


      
    }
}