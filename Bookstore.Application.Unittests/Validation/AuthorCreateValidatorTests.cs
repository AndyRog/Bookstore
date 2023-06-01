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
    public class AuthorCreateValidatorTests
    {
        private AuthorCreateValidator AuthorCreateValidator { get; } = new AuthorCreateValidator();

        [Fact]
        public void Valid_AuthorCreate_Passes_Validation()
        {
            //Arrange
            var AuthorCreate = new AuthorCreate("Test", "Test");

            //Act
            var result = AuthorCreateValidator.Validate(AuthorCreate);

            //Assert
            Assert.True(result.IsValid);
        
        }
        
        [Fact]
        public void Validation_Error_For_Emty_FirstName()
        {
            //Arrange
            var AuthorCreate = new AuthorCreate(string.Empty,"test" );

            //Act
            var result = AuthorCreateValidator.Validate(AuthorCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmptyValidator") && error.PropertyName.Equals("FirstName"));
        
        } 
        
        [Fact]
        public void Validation_Error_For_Emty_LastName()
        {
            //Arrange
            var AuthorCreate = new AuthorCreate("test", string.Empty );

            //Act
            var result = AuthorCreateValidator.Validate(AuthorCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("NotEmptyValidator") && error.PropertyName.Equals("LastName"));
        
        }
        
        [Fact]
        public void Validation_Error_For_Too_Long_FirstName()
        {
            //Arrange
            var authorCreate = new AuthorCreate(@"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "test");

            //Act
            var result = AuthorCreateValidator.Validate(authorCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error=>error.ErrorCode.Equals("MaximumLengthValidator") && error.PropertyName.Equals("FirstName"));
        
        }
       
        [Fact]
        public void Validation_Error_For_Too_Long_LastName()
        {
            //Arrange
            var authorCreate = new AuthorCreate("test", @"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");

            //Act
            var result = AuthorCreateValidator.Validate(authorCreate);

            //Assert
            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, error => error.ErrorCode.Equals("MaximumLengthValidator") && error.PropertyName.Equals("LastName"));

        }
        

    }
}
