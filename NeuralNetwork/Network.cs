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
        /// Expected answer when the network learns
        /// </summary>
        private Matrix Expected { get; set; }

        /// <summary>
        /// Intermediary Matrix for Activations computation
        /// </summary>
        private List<Matrix> Zmatrices { get; set; }

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
        /// Number of layers
        /// </summary>
        public int NumberOfLayer { get; set; }

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

            List<Matrix> zeros = biases.Select(b => new Matrix(new float[b.mat.GetLength(0), b.mat.GetLength(1)])).ToList();

            Activations = new List<Matrix> { Inputs };
            Activations.AddRange(new List<Matrix>(zeros));

            Zmatrices = new List<Matrix>(zeros);

            Feedfoward();
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

            Feedfoward();
        }

        private void InitializeNetwork(List<int> Sizes)
        {
            NumberOfLayer = Sizes.Count;
            Activations = new List<Matrix>();
            Zmatrices = new List<Matrix>();
            Biases = new List<Matrix>();
            Weights = new List<Matrix>();

            Random rbiases = new Random();
            Random rweights = new Random();

            // Initialize Activations
            foreach (int y in Sizes)
            {
                Activations.Add(new float[y, 1]);
                Zmatrices.Add(new float[y, 1]);
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
                Zmatrices[l - 1] = (Weights[l - 1] * Activations[l - 1]) + Biases[l - 1];
                Activations[l] = Neuron.Sigmoid(Zmatrices[l]);
            }
        }

        /// <summary>
        /// Train the newtmork with the stockastic gradient descent method
        /// It seeks a (at least local) minimum value of the cost error of the network
        /// </summary>
        /// <param name="datas"></param>
        /// <param name="generations"></param>
        /// <param name="miniBatchSize"></param>
        /// <param name="eta"></param>
        /// <param name="TestData"></param>
        public void StochasticGradientDescent(List<Network> datas, int generations, int miniBatchSize, int eta, List<Network> TestData = null)
        {
            int n = datas.Count;

            for (int j = 0; j < generations; j++)
            {
                datas.Shuffle();

                List<List<Network>> miniBatches = new List<List<Network>>();

                for (int k = 0; k < n; k += miniBatchSize)
                {
                    var miniBatch = datas.GetRange(k, k + miniBatchSize);
                    miniBatches.Add(miniBatch);
                }

                foreach (List<Network> miniBatch in miniBatches)
                {
                    UpdateMiniBatch(miniBatch, eta);
                }

                //Feedfoward();
                //float[] arr = Activations.Cast<float>().ToArray();
                //float max = arr.Max();
                //int maxIndex = Array.IndexOf(arr, max);
                //Console.WriteLine($"genration {j} gives the answer {maxIndex} with {max} accuracy");

                Console.WriteLine($"genration {j} complete");
            }
        }

        /// <summary>
        /// Update a mini batch of results representing a small portion of the neural network
        /// </summary>
        /// <param name="miniBatch">small batch of datas</param>
        /// <param name="eta">leaning rate</param>
        private void UpdateMiniBatch(List<Network> miniBatch, float eta)
        {
            List<Matrix> nablaBiases = Biases.Select(b => new Matrix(new float[b.mat.GetLength(0), b.mat.GetLength(1)])).ToList();
            List<Matrix> nablaWeights = Weights.Select(w => new Matrix(new float[w.mat.GetLength(0), w.mat.GetLength(1)])).ToList();

            for (int n = 0; n < miniBatch.Count; n++)
            {
                DeltaNabla deltaNabla = BackPropagation(miniBatch[n]);
                for (int l = 0; l < NumberOfLayer; l++)
                {
                    nablaBiases[l] = nablaBiases[l] + deltaNabla.Biases[l];
                    nablaWeights[l] = nablaWeights[l] + deltaNabla.Weights[l];
                }
            }

            float K = eta / miniBatch.Count;

            for (int l = 0; l < NumberOfLayer; l++)
            {
                Biases[l] = Biases[l] - K * nablaBiases[l];
                Weights[l] = Weights[l] - K * nablaWeights[l];
            }
        }

        /// <summary>
        /// Propagate delta cost through the network to recompute weights and biases
        /// </summary>
        /// <param name="network">network used as sample to learn</param>
        /// <returns>gradient of weight and biases</returns>
        private DeltaNabla BackPropagation(Network network)
        {
            List<Matrix> nablaBiases = Biases.Select(b => new Matrix(new float[b.mat.GetLength(0), b.mat.GetLength(1)])).ToList();
            List<Matrix> nablaWeights = Weights.Select(w => new Matrix(new float[w.mat.GetLength(0), w.mat.GetLength(1)])).ToList();

            Matrix delta = network.CostDerivative() * Neuron.SigmoidPrime(network.Zmatrices.Last());

            nablaBiases[nablaBiases.Count - 1] = delta;
            nablaWeights[nablaWeights.Count - 1] = delta * network.Activations[network.Activations.Count - 2].Transpose();

            for (int l = network.NumberOfLayer - 1; l > 0; l--)
            {
                delta = (network.Weights[l + 1].Transpose() * delta) * Neuron.SigmoidPrime(network.Zmatrices[l]);
                nablaBiases[l] = delta;
                nablaWeights[l] = delta * network.Activations[l - 1].Transpose();
            }

            return new DeltaNabla { Biases = nablaBiases, Weights = nablaWeights };
        }

        //public void Evaluate() { }

        /// <summary>
        /// Compute cost derivative : dCx/da
        /// </summary>
        /// <returns>dCx/da</returns>
        public Matrix CostDerivative()
        {
            return Activations.Last() - Expected;
        }
    }

    struct DeltaNabla
    {
        public List<Matrix> Biases;
        public List<Matrix> Weights;
    }
}
