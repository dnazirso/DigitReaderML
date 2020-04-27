using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class FeeddowardTests
    {
        [Fact]
        public void FeedowardNominalBehavior()
        {
            // Arrange
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
            var size = new List<int> { inputs.GetLength(0), 8, 8, 1 };
            Network network = new Network(size);

            // Act
            var neurons = network.Feedfoward(inputs);

            // Assert
            Assert.True(neurons.Activations[1].mat.Cast<double>().All(a => a != 0));
        }
    }
}
