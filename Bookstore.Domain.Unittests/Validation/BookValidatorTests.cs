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
        
        [Fact]
        public void Validation_Error_For_Missing_Author()
        {
            //Arrange

            var book = new Book()
            {
                Id = 1,
                //Author = Author,
                Isbn = "1234567891234",
                Titel = "Test"
            };

            //Act
            var result = BookValidator.Validate(book);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotNullValidator") && error.PropertyName.Equals("Author"));
        }
        
        
        [Fact]
        public void Validation_Error_For_Negative_Quantity()
        {
            //Arrange

            var book = new Book()
            {
                Id = 1,
                Author = Author,
                Isbn = "1234567891234",
                Titel = "Test",
                Quantity = -1
            };

            //Act
            var result = BookValidator.Validate(book);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("GreaterThanOrValidator") && error.PropertyName.Equals("Quantity"));
        }
        
        [Theory]
        [InlineData("")] //Length - Empty-Null
        [InlineData("1")] //Length - 1
        [InlineData("123456789123")] //Length - 12
        [InlineData("12345678912345")] //Length - 14
        public void Validation_Error_For_Isbn_Of_Wrong_Length(string isbn)
        {
            //Arrange

            var book = new Book()
            {
                Id = 1,
                Author = Author,
               //Isbn = "1234567891234",
               Isbn = isbn,
                Titel = "Test",
                //Quantity = -1
                Quantity = 0
            };

            //Act
            var result = BookValidator.Validate(book);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("ExactLengthValidator") && error.PropertyName.Equals("Isbn"));
        }
    }
}