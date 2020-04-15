﻿using NeuralNetwork;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NeuralNetworkTests.NetworkTests
{
    public class ConstructionTests
    {
        [Fact]
        public void NetworkHasASizes()
        {
            // Arrange
            var sizes = new List<int> { 3, 2, 1 };

            // Act
            var network = new Network(sizes);

            // Assert
            Assert.True(sizes.SequenceEqual(network.Sizes));
        }

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
    }
}
