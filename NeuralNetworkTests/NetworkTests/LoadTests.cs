using NeuralNetwork;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class LoadTests
    {
        [Fact]
        public void LoadNominalBehavior()
        {
            // Arrange
            var network = new Network(new int[] { 3, 2, 1 });

            // Act
            network.Load("memory.json");

            // Assert
        }
    }
}
