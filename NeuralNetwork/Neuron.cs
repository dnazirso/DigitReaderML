using Algebra;
using System;

namespace NeuralNetwork
{
    /// <summary>
    /// Contain Neuron operations and intermediary <see cref="Matrix"/>s
    /// </summary>
    public struct Neuron
    {
        /// <summary>
        /// List of Activations <see cref="Matrix"/>s
        /// </summ
        public Matrix[] Activations;

        /// <summary>
        /// Intermediary <see cref="Matrix"/>s for Activations computation
        /// </summary>
        public Matrix[] Zmatrices;

        /// <summary>
        /// Sigmoid function
        /// </summary>
        /// <param name="z">data result of previous neuron layer</param>
        /// <returns>a double</returns>
        public static double Sigmoid(double z) => 1.0f / (1.0f + Math.Exp(-z));

        /// <summary>
        /// Sigmoid function
        /// </summary>
        /// <param name="Z">data result of previous neuron layer</param>
        /// <returns>a <see cref="Matrix"/></returns>
        public static Matrix Sigmoid(Matrix Z)
        {
            int maxi = Z.mat.GetLength(0);
            int maxj = Z.mat.GetLength(1);

            double[,] A = new double[maxi, maxj];

            for (int i = 0; i < maxi; i++)
            {
                for (int j = 0; j < maxj; j++)
                {
                    A[i, j] = Sigmoid(Z.mat[i, j]);
                }
            }

            return A;
        }

        /// <summary>
        /// Derivative of a sigmoid function
        /// </summary>
        /// <param name="z">data result of previous neuron layer</param>
        /// <returns>a double</returns>
        public static double SigmoidPrime(double z) => Sigmoid(z) * (1 - Sigmoid(z));

        /// <summary>
        /// Derivative of a sigmoid function
        /// </summary>
        /// <param name="Z">data result of previous neuron layer</param>
        /// <returns>a <see cref="Matrix"/></returns>
        public static Matrix SigmoidPrime(Matrix Z)
        {
            int maxi = Z.mat.GetLength(0);
            int maxj = Z.mat.GetLength(1);

            double[,] A = new double[maxi, maxj];

            for (int i = 0; i < maxi; i++)
            {
                for (int j = 0; j < maxj; j++)
                {
                    A[i, j] = SigmoidPrime(Z.mat[i, j]);
                }
            }

            return A;
        }
    }
}
