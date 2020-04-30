using Algebra;
using Xunit;

namespace AlgebraTests.MatrixTests
{
    public class HadamarTest
    {
        [Fact]
        public void HadamarNominalBehavior()
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
                { 1f, 0f },
                { 0f, 4f },
            };

            // Act
            var C = A % B;

            // Assert
            Assert.Equal(expected, C);
        }
    }
}
