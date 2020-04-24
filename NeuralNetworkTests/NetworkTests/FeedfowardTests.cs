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
            var hiddens = new List<int> { 8, 8, 1 };
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
            var data = new Data { Inputs = inputs, Expected = expectedAns, Id = "test" };
            var size = new List<int> { inputs.Length };
            size.AddRange(hiddens);
            Network network = new Network(size);

            // Act
            network.Activations[0] = inputs;
            network.Feedfoward();

            // Assert
            Assert.True(network.Activations[1].mat.Cast<float>().All(a => a != 0));
        }
    }
}
