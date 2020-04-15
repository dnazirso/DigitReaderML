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
            Matrix A = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            };

            Matrix expected = new float[,]
            {
                { 1f, 3f },
                { 2f, 4f }
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
            Matrix A = new float[,]
            {
                { 1f },
                { 3f }
            };

            Matrix expected = new float[,]
            {
                { 1f, 3f },
            };

            // Act
            var C = A.Transpose();

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
