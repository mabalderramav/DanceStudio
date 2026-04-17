using FluentAssertions;

namespace DanceStudio.DomainUnitTest
{
    public class MyFirstTests
    {
        [Fact]
        public void AddTwoNumbersShouldReturnSum()
        {
            //Arrange
            const int a = 2;
            const int b = 3;
            const int expected = 5;

            //Act
            const int result = a + b;

            //Assert
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData(1,2,3,6)]
        [InlineData(5,7,9,21)]
        [InlineData(10,20,30,60)]
        public void AddThreeNumbersShouldReturnSum(int a, int b, int c, int expected)
        {
            //Arrange
            
            //Act
            var result = a + b + c;

            //Assert
            result.Should().Be(expected);
        }
    }
}
