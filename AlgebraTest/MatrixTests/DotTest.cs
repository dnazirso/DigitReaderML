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
            Matrix A = new Matrix(new float[,]
            {
                { 1f, 2f },
                { 3f, 4f }
            });

            Matrix B = new Matrix(new float[,]
            {
                { 1f, 0f ,0f},
                { 0f, 1f ,0f},
                { 0f, 0f ,1f}
            });

            // Act
            var C = A * B;

            // Assert
            Assert.Equal(A, C);
        }
    }
}
