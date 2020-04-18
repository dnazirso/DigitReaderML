using NeuralNetwork;
using System.Collections.Generic;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class ConstructionTests
    {
        [Fact]
        public void NetworkHasCorrectNumberOfLayer()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };

            // Act
            var network = new Network(sizes);

            // Assert
            Assert.Equal(sizes.Count, network.NumberOfLayer);
        }

        [Fact]
        public void NetworkHasNonEmptyBiasesVectors()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };

            // Act
            var network = new Network(sizes);

            // Assert
            Assert.NotEmpty(network.Biases);
        }

        [Fact]
        public void NetworkHasNonEmptyWeightMatrises()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };

            // Act
            var network = new Network(sizes);

            // Assert
            Assert.NotEmpty(network.Weights);
        }

        [Fact]
        public void NetworkHasNonEmptyActivationVectors()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };

            // Act
            var network = new Network(sizes);

            // Assert
            Assert.NotEmpty(network.Activations);
        }

    }
}
