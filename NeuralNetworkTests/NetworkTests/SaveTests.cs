using NeuralNetwork;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class SaveTests
    {
        [Fact]
        public void SaveNominalBehavior()
        {
            // Arrange
            var network = new Network(new int[] { 3, 2, 1 });

            // Act
            network.Save("memory.json");

            // Assert
        }
    }
}
