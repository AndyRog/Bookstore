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
    [TestCaseOrderer("BookStore.IntegrationTests.Utils.PriotizedOrderer", "Bookstore.IntegrationTests")]
    public class BookCreateTests : IntegrationTestsBase, IDisposable, IClassFixture<AuthorFixture>
    {
        public AuthorFixture AuthorFixture { get; }

        public BookCreateTests(WebApplicationFactory<Startup> factory, AuthorFixture authorFixture) : base(factory)
        {
            AuthorFixture = authorFixture;
        }

        [Fact]
        public async Task Success_StatusCode_For_Created_Book()
        {
            //Arrange
            DbContext.Authors.Add(AuthorFixture.Author);
            await DbContext.SaveChangesAsync();



            var bookCreate = new BookCreate("1234567891234", "Test", AuthorFixture.Author.Id, 1);

            var bookCreateJson = JsonConvert.SerializeObject(bookCreate);

            var content = new StringContent(bookCreateJson, Encoding.UTF8, "application/json");

            var expectedBook = Mapper.Map<Book>(bookCreate);

            //var response = await Client.PostAsync("/book/Create", content);
          
            //Act
            var response = await Client.PostAsync(requestUri: "/Book/Create", content);

            var responseContent = await response.Content.ReadAsStringAsync();


            //Get Book from DB
            var bookInDb = await DbContext.Books.Where(book => book.Isbn.Equals(bookCreate.ISBN)).SingleAsync();

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedBook, bookInDb);
        }


        //[Fact]
        //public async Task StatusCode_400_For_Validation()
        //{
        //    Arrange
        //    var authorCreate = new AuthorCreate("Test", string.Empty);

        //    var bookCreateJson = JsonConvert.SerializeObject(authorCreate);

        //    var content = new StringContent(bookCreateJson, Encoding.UTF8, "application/json");

        //    Act
        //    var response = await Client.PostAsync("/Author/Create", content);

        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    Assert
        //    Assert.Equal(400, (int)response.StatusCode);

        //    Assert.Contains("Validation Error", responseContent);



        //}
        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}