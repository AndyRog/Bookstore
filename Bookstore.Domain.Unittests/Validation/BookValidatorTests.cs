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
            //Arrange

            var book = new Book()
            {
                Id = 1,
                Author = Author,
                Isbn = "1234567891234",
                Titel = "Test"
            };

            //Act
            var result = BookValidator.Validate(book);

            //Assert
            Assert.True(result.IsValid);
        }
    }
}