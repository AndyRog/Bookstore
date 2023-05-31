using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Unittests.Validation
{
    public class BookUpdateValidatorTests
    {
        private BookUpdateValidator BookUpdateValidator { get; } = new BookUpdateValidator();

        [Fact]
        public void Valid_BookUpdate_Passes_Validation()
        {
            //Arrange
            var bookUpdate = new BookUpdate(1,"1234567891234", "Test", 1 );

            //Act
            var result = BookUpdateValidator.Validate(bookUpdate);

            //Assert
            Assert.True(result.IsValid);
        
        }
        
        [Fact]
        public void Validation_Error_For_Emty_Title()
        {
            //Arrange
            var bookCreate = new BookCreate("1234567891234", string.Empty, 1, 0);

            //Act
            var result = BookUpdateValidator.Validate(bookCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmptyValidator") && error.PropertyName.Equals("Title"));
        
        }
        
        
        [Fact]
        public void Validation_Error_For_Too_Long_Titel()
        {
            //Arrange
            var bookCreate = new BookCreate("1234567891234", @"AAAAAAAAAA\r\nAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", 1, 0);

            //Act
            var result = BookUpdateValidator.Validate(bookCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("MaximumLengthValidator") && error.PropertyName.Equals("Title"));
        
        }
    }
}
