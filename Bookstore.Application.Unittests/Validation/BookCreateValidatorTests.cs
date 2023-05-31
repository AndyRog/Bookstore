using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Application.Unittests.Validation
{
    public class BookCreateValidatorTests
    {
        private BookCreateValidator BookCreateValidator { get; } = new BookCreateValidator();

        [Fact]
        public void Valid_BookCreate_Passes_Validation()
        {
            //Arrange
            var bookCreate = new BookCreate("1234567891234", "Test", 1, 0);

            //Act
            var result = BookCreateValidator.Validate(bookCreate);

            //Assert
            Assert.True(result.IsValid);
        
        }
        
        [Fact]
        public void Validation_Error_For_Emty_Titel()
        {
            //Arrange
            var bookCreate = new BookCreate("1234567891234", string.Empty, 1, 0);

            //Act
            var result = BookCreateValidator.Validate(bookCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmpyValidator") && error.PropertyName.Equals("Titel"));
        
        }
    }
}
