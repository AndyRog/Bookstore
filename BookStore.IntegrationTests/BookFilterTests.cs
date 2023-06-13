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
    public class BookFilterTests : IntegrationTestsBase, IDisposable
    {
        public Author Author { get; }
        public List<Book> Books { get; }

        public BookFilterTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
            Author = new Author()
            {
                FirstName = "Test",
                LastName = "Test"
            };

            DbContext.Authors.Add(Author);
            DbContext.SaveChanges();

            Books = new List<Book>()
            {
                new Book()
                {
                    AuthorId = Author.Id,
                    Title = "Test",
                    Isbn = "1234567891234",
                    Quantity = 1
                },
               
                new Book()
                {
                    AuthorId = Author.Id,
                    Title = "Title",
                    Isbn = "1234567891235",
                    Quantity = 1
                }
            };

            Books.ForEach(book => DbContext.Books.Add(book));
            DbContext.SaveChanges();

        }

        [Fact]
        public async Task Filterd_Result_For_SerchTerm_Is_Correct()
        {
            //Arrange
            var bookFilter = new BookFilter("Test");

            var bookFilterJson = JsonConvert.SerializeObject(bookFilter);

            var content = new StringContent(bookFilterJson, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Client.BaseAddress + "Book/Filter"),
                Content = content
            };

            //Act
            var response = await Client.SendAsync(request);

           var responseContent = await response.Content.ReadAsStringAsync();

            var responseBooks = JsonConvert.DeserializeObject<List<Book>>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Single(responseBooks!);
            Assert.Equal(Books[0], responseBooks![0]);
        }[Fact]
        public async Task All_Books_Returned_For_Emty_SerchTerm()
        {
            //Arrange
            var bookFilter = new BookFilter(string.Empty);

            var bookFilterJson = JsonConvert.SerializeObject(bookFilter);

            var content = new StringContent(bookFilterJson, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(Client.BaseAddress + "Book/Filter"),
                Content = content
            };

            //Act
            var response = await Client.SendAsync(request);

           var responseContent = await response.Content.ReadAsStringAsync();

            var responseBooks = JsonConvert.DeserializeObject<List<Book>>(responseContent);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(Books.Count, responseBooks!.Count);
            Assert.Equal(Books[0], responseBooks![0]);

            Books.ForEach(bookInDb => Assert.Contains(responseBooks, rb => rb.Equals(bookInDb)));
        }

     

        public void Dispose()
        {
            DbContext.Authors.Remove(Author);
            DbContext.SaveChanges();
            DbContext.Dispose();
        }



    }
}