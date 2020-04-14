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
                { 0f, 2f },
                { 3f, 3f },
            };

            // Act
            var C = A - B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
