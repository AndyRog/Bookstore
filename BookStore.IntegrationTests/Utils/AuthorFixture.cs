using Bookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.IntegrationTests.Utils
{
    public class AuthorFixture
    {
        public Author Author { get; }
        public AuthorFixture()
        {
            Author = new Author()
            {
                FirstName = "Test",
                LastName = "Test"
            };
        }

    }
}
