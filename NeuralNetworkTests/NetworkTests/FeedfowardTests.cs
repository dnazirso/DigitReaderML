using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class FeeddowardTests
    {
        [Fact]
        public void FeeddowardNominalBehavior()
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
            network.Feedfoward(inputs);

            // Assert
            Assert.True(network.Activations[1].mat.Cast<double>().All(a => a != 0));
        }
    }
}
