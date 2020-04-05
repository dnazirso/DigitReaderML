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
            float[,] A = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            };

            float[,] B = new float[,]
            {
                { 1f, 0f },
                { 0f, 1f },
            };

            float[,] expected = new float[,]
            {
                { 2f, 2f },
                { 3f, 5f },
            };

            // Act
            var C = Matrix.Add(A, B);

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
