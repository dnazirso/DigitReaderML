using Algebra;
using Xunit;

namespace AlgebraTests.MatrixTests
{
    public class AddTest
    {
        [Fact]
        public void AddNominalBehavior()
        {
            // Arrange
            Matrix A = new double[,]
            {
                { 1d, 2d },
                { 3d, 4d }
            };

            Matrix B = new double[,]
            {
                { 1d, 0d },
                { 0d, 1d },
            };

            Matrix expected = new double[,]
            {
                { 2d, 2d },
                { 3d, 5d },
            };

            // Act
            var C = A + B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
