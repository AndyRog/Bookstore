using Bookstore.Application.Dtos;
using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTests;

public class AuthorCreateTests : IntegrationTestsBase, IDisposable
{
    public AuthorCreateTests(WebApplicationFactory<Startup> factory) : base(factory)
    {
        
    }

    [Fact]
    public async Task Success_StatusCode_For_Created_Author()
    {
        //Arrange
        var authorCreate = new AuthorCreate("Test", "Test");

        var authorCreateJson = JsonConvert.SerializeObject(authorCreate);

        var content = new StringContent(authorCreateJson, Encoding.UTF8,"application/json");

        var expectedAuthor = Mapper.Map<Author>(authorCreate);


        //Act
        var response = await Client.PostAsync("/Author/Create", content);

        var responseContent = await response.Content.ReadAsStringAsync();
        var authorenId = int.Parse(responseContent);

        //Get Author from DB
        var authorInDb = await DbContext.Authors.Where(author => author.Id == authorenId).SingleAsync();


        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(expectedAuthor, authorInDb);

        //Teardown
        DbContext.Authors.Remove(authorInDb);
        await DbContext.SaveChangesAsync();
        

    }

    [Fact]
    public async Task StatusCode_400_For_Validation()
    {
        //Arrange
        var authorCreate = new AuthorCreate("Test", string.Empty);

        var authorCreateJson = JsonConvert.SerializeObject(authorCreate);

        var content = new StringContent(authorCreateJson, Encoding.UTF8, "application/json");

        //Act
        var response = await Client.PostAsync("/Author/Create", content);

        var responseContent = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(400, (int)response.StatusCode);

        Assert.Contains("Validation Error", responseContent);



    }

    public void Dispose()
    {
        DbContext.Dispose();
    }
}
