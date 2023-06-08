﻿using Bookstore.Application.Dtos;
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

public class AuthorCreateTests : IntegrationTestsBase
{
    public AuthorCreateTests(WebApplicationFactory<Startup> factory) : base(factory)
    {
        
    }

    [Fact]
    public async Task Success_Status_For_Created_Author()
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

    }
}
