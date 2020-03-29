using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    public class Network
    {
        public List<int> Sizes { get; set; }
        public int NumberOfLayer { get; set; }
        public List<float[,]> Biases { get; set; }
        public List<float[,]> Weights { get; set; }
        public Network(List<int> Sizes)
        {

            this.Sizes = Sizes;
            NumberOfLayer = Sizes.Count;

            Random rbiases = new Random();
            Random rweights = new Random();

            // Initialize Biases
            foreach (var y in Sizes.Skip(1))
            {
                var biases = new float[y, 1];
                for (var i = 0; i < y; i++)
                {
                    biases[i, 0] = (float)rbiases.NextDouble();
                }
                Biases.Add(biases);
            }

            var ziped = Sizes.SkipLast(1).Zip(Sizes.Skip(1));

            // Initialize Weights
            foreach (var (x, y) in ziped)
            {
                // x => number of neuron in previous layer
                // y => number of neuron in actual layer
                var layer = new float[y, x];
                for (var i = 0; i < y; i++)
                {
                    for (var j = 0; j < x; j++)
                    {
                        layer[i, j] = (float)rweights.NextDouble();
                    }
                    Weights.Add(layer);
                }
            }
        }

        /// <summary>
        /// Compute each data results for each layers from the input layer to the output
        /// </summary>
        /// <param name="a">previous data results of a of all layers</param>
        /// <returns>data results of a of all layers</returns>
        public List<float[,]> Feedfoward(List<float[,]> a)
        {
            foreach (var (b, w) in Biases.Zip(Weights))
            {
                //a = Neuron.Sigmoid(a);
            }
            return a;
        }

        /// <summary>
        /// Train the newtmork with the stockastic gradient descent method
        /// </summary>
        public void StochasticGradientDescent() { }

        /// <summary>
        /// Update a mini batch of results representing a small portion of the neural network
        /// </summary>
        public void UpdateMiniBatch() { }
    }
}
