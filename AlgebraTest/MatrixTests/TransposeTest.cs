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
            float[,] A = new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            };

            float[,] B = new float[,]
            {
                { 1f, 3f },
                { 2f, 4f }
            };

            // Act
            var C = Matrix.Transpose(A);

            // Assert
            Assert.Equal(C, B);
        }
    }
}
