using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Ifrastructure;

public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<Book> Books {get; set;}
    public DbSet<Author> Authors { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename =RoomBooking.db");
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Book>().HasKey(equals => equals.Id);
        builder.Entity<Book>().HasOne(equals => equals.Author).WithMany(e => e.Books).HasForeignKey(e => e.AuthorId);

        builder.Entity<Book>().Property(e => e.Title).HasMaxLength(100);
        builder.Entity<Author>().HasKey(e => e.Id);
        builder.Entity<Author>().Property(e => e.FirstName).HasMaxLength(50);
        builder.Entity<Author>().Property(e => e.LastName).HasMaxLength(50);

        base.OnModelCreating(builder);
    }
}
