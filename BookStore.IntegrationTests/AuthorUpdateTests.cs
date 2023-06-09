using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
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


}
