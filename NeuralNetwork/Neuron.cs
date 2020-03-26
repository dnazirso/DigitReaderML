using System;

namespace NeuralNetwork
{
    public static class Neuron
    {

        /// <summary>
        /// Sigmoid function
        /// </summary>
        /// <param name="z">data result of previous neuron layer</param>
        /// <returns>a float</returns>
        public static float Sigmoid(float z) => 1.0f / (1.0f + MathF.Exp(-z));

        /// <summary>
        /// Derivative of a sigmoid function
        /// </summary>
        /// <param name="z">data result of previous neuron layer</param>
        /// <returns>a float</returns>
        public static float SigmoidPrime(float z) => Sigmoid(z) * (1 - Sigmoid(z));
    }
}
