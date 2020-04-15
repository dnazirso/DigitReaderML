using Algebra;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetwork
{
    /// <summary>
    /// Represent the neural network
    /// </summary>
    public class Network
    {
        /// <summary>
        /// Sizes of each layers
        /// </summary>
        public List<int> Sizes { get; set; }

        /// <summary>
        /// Number of layers
        /// </summary>
        public int NumberOfLayer { get; set; }

        /// <summary>
        /// List of Activations matrises
        /// </summary>
        public List<Matrix> Activations { get; set; } = new List<Matrix>();

        /// <summary>
        /// List of Biases vectors
        /// </summary>
        public List<Matrix> Biases { get; set; } = new List<Matrix>();

        /// <summary>
        /// List of Weights matrises
        /// </summary>
        public List<Matrix> Weights { get; set; } = new List<Matrix>();

        /// <summary>
        /// Neural network constructor
        /// </summary>
        /// <param name="Sizes"></param>
        public Network(List<int> Sizes)
        {
            this.Sizes = Sizes;
            NumberOfLayer = Sizes.Count;

            Random rbiases = new Random();
            Random rweights = new Random();

            // Initialize Activations
            foreach (var y in Sizes)
            {
                Activations.Add(new float[y, 1]);
            }

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

            // Initialize Weights
            foreach (var (x, y) in Sizes.SkipLast(1).Zip(Sizes.Skip(1)))
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
        /// Initialize input neurons with an input vector
        /// </summary>
        /// <param name="inputs">input vector</param>
        public void InitializeInputNeurons(float[] inputs)
        {
            int maxi = Sizes[0];

            for (int i = 0; i < maxi; i++)
            {
                Activations[0].mat[i, 0] = inputs[i];
            }
        }

        /// <summary>
        /// Compute each data results for each layers from the input layer to the output
        /// </summary>
        /// <param name="a">previous data results of a of all layers</param>
        /// <returns>data results of a of all layers</returns>
        public void Feedfoward()
        {
            for (int l = 1; l < NumberOfLayer + 1; l++)
            {
                Activations[l] = Neuron.Sigmoid((Activations[l - 1] * Weights[l - 1]) + Biases[l - 1]);
            }
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
