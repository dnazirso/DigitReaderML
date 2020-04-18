﻿using NeuralNetwork;
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
            var network = new Network(inputs, expectedAns, hiddens);

            // Act
            network.Feedfoward();

            // Assert
            Assert.True(network.Activations[1].mat.Cast<float>().All(a => a != 0));
        }
    }
}
