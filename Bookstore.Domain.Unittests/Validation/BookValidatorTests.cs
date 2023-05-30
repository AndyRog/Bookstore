using Bookstore.Domain.Entities;
using Bookstore.Domain.Validation;

namespace Bookstore.Domain.Unittests.Validation
{
    public class BookValidatorTests
    {
        private Author Author { get; } = new Author()
        {
            Id = 1,
            FirstName = "Test",
            LastName = "Test"
        };

        private BookValidator BookValidator{ get; }= new BookValidator();


        [Fact]
        public void Valid_Book_Passes_Validation()
        {

        }
    }
}