using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTests;

public class AuthorUpdateTests : IntegrationTestsBase
{
    private Author Author{ get; }
    public AuthorUpdateTests(WebApplicationFactory<Startup> factory) : base(factory)
    {
        Author = new Author()
        {
            FirstName = "Test",
            LastName = "Test"
        };

        DbContext.Authors.Add(Author);
        DbContext.SaveChanges();
    }

    [Fact]
    public async Task Success_StatusCode_For_Updated_Autor()
    {
        //Arrange
        var authorUpdate = new AuthorUpdate(Author.Id, "Test", "Test2");
        var authorUpdateJson = JsonConvert.SerializeObject(authorUpdate);
        var content = new StringContent(authorUpdateJson, Encoding.UTF8, "application/json");
        var expectedAuthor = Mapper.Map<Author>(authorUpdate);

        //Act
        var response = await Client.PutAsync("Author/Update", content);

        //Get Author from DB
        await DbContext.Entry(Author).ReloadAsync();

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(expectedAuthor, Author);

    }
}
