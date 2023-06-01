using Bookstore.Application.Dtos;
using Bookstore.Application.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Application.Unittests.Validation
{
    public class AuthorUpdateValidatorTests
    {
        private AuthorUpdateValidator AuthorUpdateValidator { get; } = new AuthorUpdateValidator();

        [Fact]
        public void Valid_AuthorUpdate_Passes_Validation()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1,"Test", "Test");

            //Act
            var result = AuthorUpdateValidator.Validate(authorUpdate);

            //Assert
            Assert.True(result.IsValid);
        
        }
        
        [Fact]
        public void Validation_Error_For_Emty_FirstName()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1,string.Empty,"test" );

            //Act
            var result = AuthorUpdateValidator.Validate(authorUpdate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmptyValidator") && error.PropertyName.Equals("FirstName"));
        
        } 
        
        [Fact]
        public void Validation_Error_For_Emty_LastName()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1, "test", string.Empty);

            //Act
            var result = AuthorUpdateValidator.Validate(authorUpdate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmptyValidator") && error.PropertyName.Equals("LastName"));
        
        }
        
        [Fact]
        public void Validation_Error_For_Too_Long_FirstName()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1, @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "test");

            //Act
            var result = AuthorUpdateValidator.Validate(authorUpdate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("MaximumLengthValidator") && error.PropertyName.Equals("FirstName"));
        
        }
       
        [Fact]
        public void Validation_Error_For_Too_Long_LastName()
        {
            //Arrange
            var authorUpdate = new AuthorUpdate(1, "test", @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            //Act
            var result = AuthorUpdateValidator.Validate(authorUpdate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error => error.ErrorCode.Equals("MaximumLengthValidator") && error.PropertyName.Equals("LastName"));

        }
        

    }
}
