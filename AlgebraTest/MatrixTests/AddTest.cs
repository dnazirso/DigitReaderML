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

            Matrix expected = new float[,]
            {
                { 2f, 2f },
                { 3f, 5f },
            };

            // Act
            var C = A + B;

            // Assert
            Assert.Equal(expected, C);
        }

        [Fact]
        public void AddIdentityNominalBehavior()
        {
            // Arrange
            float A = 1;

            Matrix B = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f },
            };

            Matrix expected = new float[,]
            {
                { 2f, 3f },
                { 4f, 5f },
            };

            // Act
            var C = A + B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
