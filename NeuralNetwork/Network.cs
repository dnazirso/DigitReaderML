using Algebra;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NeuralNetwork
{
    /// <summary>
    /// Represent the neural network
    /// </summary>
    public class Network
    {
        /// <summary>
        /// List of Biases vectors
        /// </summary>
        public Matrix[] Biases { get; private set; }

        /// <summary>
        /// List of Weights matrises
        /// </summary>
        public Matrix[] Weights { get; private set; }

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
        /// Initialize a <see cref="Network"/> from a given size
        /// </summary>
        /// <param name="Sizes"></param>
        private void InitializeNetwork(List<int> Sizes)
        {
            NumberOfLayer = Sizes.Count;
            Biases = new Matrix[Sizes.Count - 1];
            Weights = new Matrix[Sizes.Count - 1];
            int n = 0;

            // Initialize Biases
            foreach (int y in Sizes.Skip(1))
            {
                double[,] biases = new double[y, 1];
                for (int i = 0; i < y; i++)
                {
                    biases[i, 0] = ThreadSafeRandom.NormalRand();
                }
                Biases[n] = biases;
                n++;
            }

            n = 0;

            // Initialize Weights
            foreach ((int x, int y) in Sizes.SkipLast(1).Zip(Sizes.Skip(1)))
            {
                // x => number of neuron in previous layer
                // y => number of neuron in actual layer
                double[,] layer = new double[y, x];
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        layer[i, j] = ThreadSafeRandom.NormalRand();
                    }
                }
                Weights[n] = layer;
                n++;
            }
        }

        /// <summary>
        /// Compute each data results for each layers from the input layer to the output
        /// </summary>
        /// <param name="a">previous data results of a of all layers</param>
        /// <returns>data results of a of all layers</returns>
        public Neuron Feedfoward(Matrix inputs)
        {
            Matrix[] activations = new Matrix[NumberOfLayer];
            Matrix[] zmatrices = new Matrix[NumberOfLayer - 1];
            activations[0] = inputs;

            for (int l = 0; l < NumberOfLayer - 1; l++)
            {
                zmatrices[l] = (Weights[l] * activations[l]) + Biases[l];
                activations[l + 1] = Neuron.Sigmoid(zmatrices[l]);
            }

            return new Neuron { Activations = activations, Zmatrices = zmatrices };
        }

        /// <summary>
        /// Train the <see cref="Network"/> with the stockastic gradient descent method
        /// It seeks a (at least local) minimum value of the cost error of the network
        /// </summary>
        /// <param name="datas">Inputs list</param>
        /// <param name="generations">Number of generation that will train</param>
        /// <param name="miniBatchSize">Size of a batch of data</param>
        /// <param name="eta">Learning rate</param>
        /// <param name="TestData">Data that will test the Network accuracy</param>
        public void StochasticGradientDescent(List<Data> datas, int generations, int miniBatchSize, double eta, List<Data> TestData = null)
        {
            Console.WriteLine("\nbegining learing using the stochastic gradient descent method");
            Console.WriteLine($"\nbatch size {miniBatchSize}, learning rate {eta}, number of generation {generations}");

            for (int j = 0; j < generations; j++)
            {
                datas.Shuffle();

                List<List<Data>> miniBatches = datas.ChunkBy(miniBatchSize);

                foreach (List<Data> miniBatch in miniBatches)
                {
                    UpdateMiniBatch(miniBatch, eta);
                }

                if (TestData != null)
                {
                    Console.WriteLine($"generation {j} success rate is {Evaluate(TestData)} / {TestData.Count}");
                    Trace.WriteLine($"generation {j} success rate is {Evaluate(TestData)} / {TestData.Count}");
                }
                else
                {
                    Console.WriteLine($"generation {j} complete");
                    Trace.WriteLine($"generation {j} complete");
                }
            }
        }

        /// <summary>
        /// Update a mini batch of results representing a small portion of the neural network
        /// </summary>
        /// <param name="miniBatch">small batch of datas</param>
        /// <param name="eta">leaning rate</param>
        private void UpdateMiniBatch(List<Data> miniBatch, double eta)
        {
            Matrix[] nablaBiases = Biases.Select(b => new Matrix(new double[b.mat.GetLength(0), b.mat.GetLength(1)])).ToArray();
            Matrix[] nablaWeights = Weights.Select(w => new Matrix(new double[w.mat.GetLength(0), w.mat.GetLength(1)])).ToArray();

            double K = eta / miniBatch.Count;

            for (int n = 0; n < miniBatch.Count; n++)
            {
                DeltaNabla deltaNabla = BackPropagation(miniBatch[n]);
                for (int l = 0; l < NumberOfLayer - 1; l++)
                {
                    nablaBiases[l] += deltaNabla.Biases[l];
                    nablaWeights[l] += deltaNabla.Weights[l];
                }
            }

            for (int l = 0; l < NumberOfLayer - 1; l++)
            {
                Biases[l] -= K * nablaBiases[l];
                Weights[l] -= K * nablaWeights[l];
            }
        }

        /// <summary>
        /// Propagate delta cost through the network to recompute weights and biases
        /// </summary>
        /// <param name="data">network used as sample to learn</param>
        /// <returns>gradient of weight and biases</returns>
        private DeltaNabla BackPropagation(Data data)
        {
            Trace.WriteLine($"Backpropagation on {data.Id}");

            Matrix[] deltaNablaBiases = Biases.Select(b => new Matrix(new double[b.mat.GetLength(0), b.mat.GetLength(1)])).ToArray();
            Matrix[] deltaNablaWeights = Weights.Select(w => new Matrix(new double[w.mat.GetLength(0), w.mat.GetLength(1)])).ToArray();

            Neuron neurons = Feedfoward(data.Inputs);

            Matrix delta = CostDerivative(neurons.Activations[^1], data.Expected) * Neuron.SigmoidPrime(neurons.Zmatrices[^1]);

            deltaNablaBiases[^1] = delta;
            deltaNablaWeights[^1] = delta * neurons.Activations[^2].Transpose();

            for (int l = NumberOfLayer - 2; l > 0; l--)
            {
                delta = (Weights[l].Transpose() * delta) * Neuron.SigmoidPrime(neurons.Zmatrices[l - 1]);
                deltaNablaBiases[l - 1] = delta;
                deltaNablaWeights[l - 1] = delta * neurons.Activations[l - 1].Transpose();
            }

            return new DeltaNabla { Biases = deltaNablaBiases, Weights = deltaNablaWeights };
        }

        /// <summary>
        /// Evaluate accuracy of the <see cref="Network"/> to give an expected answer
        /// </summary>
        /// <param name="TestData">list of data for tests</param>
        /// <returns>number of succeeded</returns>
        private int Evaluate(List<Data> TestData)
        {
            int suceeded = 0;
            foreach (Data data in TestData)
            {
                Trace.WriteLine($"Evaluation of {data.Id}");

                Neuron neurons = Feedfoward(data.Inputs);

                double[] arrans = neurons.Activations.Last().mat.Cast<double>().ToArray();
                double maxans = arrans.Max();
                int maxIndexAns = Array.IndexOf(arrans, maxans);

                double[] arrexp = data.Expected.mat.Cast<double>().ToArray();
                double maxexp = arrexp.Max();
                int maxIndexExp = Array.IndexOf(arrexp, maxexp);

                bool hasSucceded = maxIndexAns == maxIndexExp;

                suceeded += hasSucceded ? 1 : 0;
                Trace.WriteLine($"has succeded : {hasSucceded}");
            }

            return suceeded;
        }

        /// <summary>
        /// Compute the cost derivative : dCx/da
        /// </summary>
        /// <param name="Expected">Expected answer</param>
        /// <returns>dCx/da</returns>
        private Matrix CostDerivative(Matrix output, Matrix Expected)
        {
            return output - Expected;
        }
    }

    /// <summary>
    /// Nabla structure for computation
    /// </summary>
    struct DeltaNabla
    {
        public Matrix[] Biases;
        public Matrix[] Weights;
    }
}
