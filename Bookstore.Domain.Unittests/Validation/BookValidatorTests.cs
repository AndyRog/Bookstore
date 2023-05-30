using Bookstore.Domain.Entities;

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

        [Fact]
        public void Test1()
        {

        }
    }
}