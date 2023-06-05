using Bookstore.Application.Contracts;
using Bookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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


        public async Task<long> AddAuthorAsync(Author author)
        {
            await DbContext.Authors.AddAsync(author);
            await DbContext.SaveChangesAsync();
            return author.Id;
        }

        public async Task<Author?> GetAuthorByIdAsync(long authorId)
        {
            return await DbContext.Authors.Where(author => author.Id == authorId).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
