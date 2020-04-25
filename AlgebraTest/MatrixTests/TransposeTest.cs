using Algebra;
using Xunit;

namespace AlgebraTests.MatrixTests
{
    public class TransposeTest
    {
        [Fact]
        public void TransposeNominalBehavior()
        {
            // Arrange
            Matrix A = new double[,]
            {
                { 1d, 2d },
                { 3d, 4d }
            };

            Matrix expected = new double[,]
            {
                { 1d, 3d },
                { 2d, 4d }
            };

            // Act
            var C = A.Transpose();

            // Assert
            Assert.Equal(expected, C);
        }

        [Fact]
        public void TransposeVector()
        {
            // Arrange
            Matrix A = new double[,]
            {
                { 1d },
                { 3d }
            };

            Matrix expected = new double[,]
            {
                { 1d, 3d },
            };

            // Act
            var C = A.Transpose();

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
