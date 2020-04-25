using NeuralNetwork;
using Xunit;

namespace NeuralNetworkTests.NeuronTests
{
    public class SigmoidTest
    {
        [Fact]
        public void SigmoidNominalBehavior()
        {
            // Arrange
            var z = 0d;

            // Act
            var neuron = Neuron.Sigmoid(z);

            // Assert
            Assert.Equal(0.5f, neuron);
        }
    }
}
