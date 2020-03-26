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
            float[,] A = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            };
            // Arrange
            float[,] B = new float[,]
            {
                { 1f, 0f ,0f},
                { 0f, 1f ,0f},
                { 0f, 0f ,1f}
            };

            // Act
            var C = Matrix.Dot(A, B);

            // Assert
            Assert.Equal(A, C);
        }
    }
}
