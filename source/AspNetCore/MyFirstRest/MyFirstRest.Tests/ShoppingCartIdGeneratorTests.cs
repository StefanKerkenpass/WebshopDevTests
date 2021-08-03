using MyFirstRest.Model;
using Xunit;

namespace MyFirstRest.Tests
{
    public class ShoppingCartIdGeneratorTests
    {
        [Fact]
        public void GeneratedId_WhenCalled_ReturnsNonEmptyString()
        {
            var sut = new ShoppingCartIdGenerator();

            var actual = sut.GenerateId();
            
            Assert.NotNull(actual);
            Assert.NotEqual(string.Empty, actual);
        }
        
        [Fact]
        public void GeneratedId_WhenCalled_Returns20Characters()
        {
            var sut = new ShoppingCartIdGenerator();

            var actual = sut.GenerateId();
            
            Assert.True( actual.Length == 20);
        }
        
        [Fact]
        public void GeneratedId_WhenCalledTwice_ReturnsDifferentIds()
        {
            var sut = new ShoppingCartIdGenerator();
            var actual = sut.GenerateId();
            
            var actual2 = sut.GenerateId();
            
            Assert.NotEqual(actual, actual2);
        }
    }
}