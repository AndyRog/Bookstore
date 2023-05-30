namespace Bookstore.Domain.Entities;

public class Book
{
    public long Id { get; set; }
    public string Titel { get; set; } = default!;
    public string Isbn { get; set; } = default!;
    public int Quantity { get; set; }
    public long AuthorId { get; set; }
    public Author Author { get; set; } = default!;
}