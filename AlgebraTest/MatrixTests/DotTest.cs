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
            Matrix A = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            };

            Matrix B = new float[,]
            {
                { 1f, 0f },
                { 0f, 1f },
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
            Matrix A = new float[,]
            {
                { 1f, 2f ,3f },
                { 4f, 5f ,6f }
            };

            Matrix B = new float[,]
            {
                { 1f },
                { 1f },
                { 1f }
            };

            Matrix expected = new float[,]
            {
                { 6f },
                { 15f }
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
            Matrix A = new float[,]
            {
                { 1f, 2f ,3f },
                { 4f, 5f ,6f }
            };

            float B = 2;

            Matrix expected = new float[,]
            {
                { 2f, 4f ,6f },
                { 8f, 10f ,12f }
            };

            // Act
            var C = A * B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
