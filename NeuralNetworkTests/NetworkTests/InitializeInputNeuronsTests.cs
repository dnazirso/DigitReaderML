using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class InitializeInputNeuronsTests
    {
        [Fact]
        public void InitializeInputNeuronsNominalBehavior()
        {
            // Arrange
            var sizes = new List<int> { 4, 8, 8, 8, 10 };
            var inputs = new float[] { 0.1f, 1f, 1f, 0.1f };

            // Act
            var network = new Network(sizes);
            network.InitializeInputNeurons(inputs);

            // Assert
            Assert.True(network.Activations[0].mat.Cast<float>().SequenceEqual(inputs));
        }
    }
}
