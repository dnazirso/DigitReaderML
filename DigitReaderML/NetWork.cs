using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitReaderML
{
    class NetWork
    {
        public List<int> Sizes { get; set; }
        public int NumberOfLayer { get; set; }
        public List<List<float>> Biases { get; set; }
        public float[,] BiasesArr { get; set; }
        public List<List<float>> Weights { get; set; }
        public NetWork(List<int> Sizes)
        {

            this.Sizes = Sizes;
            NumberOfLayer = Sizes.Count;
            Random rbiases = new Random();
            Random rweights = new Random();

            foreach (var y in Sizes.Skip(1))
            {
                var list = new List<float>();
                for (var i = 0; i < y; i++)
                {
                    list.Add((float)rbiases.NextDouble());
                }
                Biases.Add(list);
            }

            var ziped = Sizes.SkipLast(1).Zip(Sizes.Skip(1));
            foreach (var (x, y) in ziped)
            {
                for (var i = 0; i < y; i++)
                {
                    var list = new List<float>();
                    for (var j = 0; j < x; j++)
                    {
                        list.Add((float)rweights.NextDouble());
                    }
                    Biases.Add(list);
                }
            }

        }

        public float Feedfoward(float a)
        {
            foreach (var (b, w) in Biases.Zip(Weights))
            {
                a = Neuron.Sigmoid(a);
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
