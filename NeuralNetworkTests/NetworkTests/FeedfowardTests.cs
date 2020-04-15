using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class FeedfowardTests
    {
        [Fact]
        public void FeedfowardNominalBehavior()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };
            var inputs = new float[] { 1f, 1f, 1f };
            var network = new Network(sizes);
            network.InitializeInputNeurons(inputs);

            // Act
            network.Feedfoward();

            // Assert
            Assert.True(network.Activations[1].mat.Cast<float>().All(a => a != 0));
        }
    }
}
