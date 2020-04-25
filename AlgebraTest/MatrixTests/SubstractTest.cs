using Algebra;
using Xunit;

namespace AlgebraTests.MatrixTests
{
    public class SubstractTest
    {
        [Fact]
        public void MinusNominalBehavior()
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
                { 0d, 2d },
                { 3d, 3d },
            };

            // Act
            var C = A - B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
