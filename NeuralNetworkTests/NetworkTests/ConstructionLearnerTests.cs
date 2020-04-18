using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class ConstructionLearnerTests
    {
        [Fact]
        public void InitializeInputNeuronsNominalBehavior()
        {
            // Arrange
            var hiddens = new List<int> { 8, 8 };
            var inputs = new float[,]
            {
                { 0.1f },
                { 1f },
                { 1f },
                { 0.1f }
            };
            var expectedAns = new float[,]
            {
                { 0 },
            };

            // Act
            var network = new Network(inputs, expectedAns, hiddens);

            // Assert
            Assert.True(network.Activations[0].mat.Cast<float>().SequenceEqual(inputs.Cast<float>()));
        }
    }
}
