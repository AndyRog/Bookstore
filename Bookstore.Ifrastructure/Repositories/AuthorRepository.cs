using Bookstore.Application.Contracts;
using Bookstore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Ifrastructure.Repositories
{
    internal class AuthorRepository : IAuthorRepository
    {
        public ApplicationDbContext DbContext { get; }
        
        public AuthorRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }


        public Task<long> AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetAuthorByIdAsync(long authorId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
