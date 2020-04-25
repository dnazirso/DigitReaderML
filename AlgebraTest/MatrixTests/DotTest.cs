using Algebra;
using Xunit;

namespace AlgebraTests.MatrixTests
{
    public class DotTest
    {
        [Fact]
        public void DotNominalBehavior()
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

            // Act
            var C = A * B;

            // Assert
            Assert.Equal(A, C);
        }

        [Fact]
        public void DotVector()
        {
            // Arrange
            Matrix A = new double[,]
            {
                { 1d, 2d ,3d },
                { 4d, 5d ,6d }
            };

            Matrix B = new double[,]
            {
                { 1d },
                { 1d },
                { 1d }
            };

            Matrix expected = new double[,]
            {
                { 6d },
                { 15d }
            };

            // Act
            var C = A * B;

            // Assert
            Assert.Equal(expected, C);
        }

        [Fact]
        public void DotReal()
        {
            // Arrange
            Matrix A = new double[,]
            {
                { 1d, 2d ,3d },
                { 4d, 5d ,6d }
            };

            double B = 2;

            Matrix expected = new double[,]
            {
                { 2d, 4d ,6d },
                { 8d, 10d ,12d }
            };

            // Act
            var C = A * B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
