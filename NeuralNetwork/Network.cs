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
        /// Number of layers
        /// </summary>
        public int NumberOfLayer { get; set; }

        /// <summary>
        /// Expected answer when the network learns
        /// </summary>
        private Matrix Expected { get; set; }

        /// <summary>
        /// List of Activations matrises
        /// </summary>
        public List<Matrix> Activations { get; private set; }

        /// <summary>
        /// List of Biases vectors
        /// </summary>
        public List<Matrix> Biases { get; private set; }

        /// <summary>
        /// List of Weights matrises
        /// </summary>
        public List<Matrix> Weights { get; private set; }

        /// <summary>
        /// Neural network constructor for custom purpose
        /// </summary>
        /// <param name="Sizes">List number of neuron per layer</param>
        public Network(List<int> Sizes)
        {
            InitializeNetwork(Sizes);
        }

        /// <summary>
        /// Neural network constructor for use
        /// </summary>
        /// <param name="Inputs">Input values</param>
        /// <param name="Weights">Weight matrises from previous learning</param>
        /// <param name="Biases">Biases matrises from previous learning</param>
        public Network(float[,] Inputs, List<float[,]> Weights, List<float[,]> Biases)
        {
            List<Matrix> biases = Biases.Select(b => (Matrix)b).ToList();
            this.Weights = Weights.Select(w => (Matrix)w).ToList();
            this.Biases = biases;
            Activations = new List<Matrix> { Inputs };
            Activations.AddRange(biases);
        }

        /// <summary>
        /// Neural network constructor for learning
        /// </summary>
        /// <param name="Inputs">Input values</param>
        /// <param name="Expected">Expected answer</param>
        /// <param name="HiddenLayers">Hidden Layers</param>
        public Network(float[,] Inputs, float[,] Expected, List<int> HiddenLayers)
        {
            List<int> Sizes = new List<int> { Inputs.Length };
            Sizes.AddRange(HiddenLayers);
            Sizes.Add(Expected.Length);

            InitializeNetwork(Sizes);
            Activations[0] = Inputs;
            this.Expected = Expected;
        }

        private void InitializeNetwork(List<int> Sizes)
        {
            NumberOfLayer = Sizes.Count;
            Activations = new List<Matrix>();
            Biases = new List<Matrix>();
            Weights = new List<Matrix>();

            Random rbiases = new Random();
            Random rweights = new Random();

            // Initialize Activations
            foreach (int y in Sizes)
            {
                Activations.Add(new float[y, 1]);
            }

            // Initialize Biases
            foreach (int y in Sizes.Skip(1))
            {
                var biases = new float[y, 1];
                for (int i = 0; i < y; i++)
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
                }
                Weights.Add(layer);
            }
        }

        /// <summary>
        /// Compute each data results for each layers from the input layer to the output
        /// </summary>
        /// <param name="a">previous data results of a of all layers</param>
        /// <returns>data results of a of all layers</returns>
        public void Feedfoward()
        {
            for (int l = 1; l < NumberOfLayer; l++)
            {
                Activations[l] = Neuron.Sigmoid((Weights[l - 1] * Activations[l - 1]) + Biases[l - 1]);
            }
        }

        /// <summary>
        /// Train the newtmork with the stockastic gradient descent method
        /// </summary>
        public void StochasticGradientDescent()
        {

        }

        /// <summary>
        /// Update a mini batch of results representing a small portion of the neural network
        /// </summary>
        public void UpdateMiniBatch() { }

        public void BackPropagation() { }
        public void Evaluate() { }
        public void CostDerivative() { }
    }
}
