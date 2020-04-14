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
            Matrix A = new Matrix(new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            Matrix B = new Matrix(new float[,]
            {
                { 1f, 0f },
                { 0f, 1f },
            });

            Matrix expected = new Matrix(new float[,]
            {
                { 2f, 2f },
                { 3f, 5f },
            });

            // Act
            var C = A + B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
