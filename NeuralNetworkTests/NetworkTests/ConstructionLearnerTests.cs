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
            var hiddens = new List<int> { 8, 8, 1 };
            var inputs = new double[,]
            {
                { 0.1d },
                { 1d },
                { 1d },
                { 0.1d }
            };
            var expectedAns = new double[,]
            {
                { 0 },
            };
            var data = new Data { Inputs = inputs, Expected = expectedAns, Id = "test" };
            var size = new List<int> { inputs.Length };
            size.AddRange(hiddens);
            Network network = new Network(size);

            // Act
            network.Activations[0] = inputs;

            // Assert
            Assert.True(network.Activations[0].mat.Cast<double>().SequenceEqual(inputs.Cast<double>()));
        }
    }
}
