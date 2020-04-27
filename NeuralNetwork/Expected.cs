using Algebra;
using System.Collections.Generic;

namespace NeuralNetwork
{
    /// <summary>
    /// Expected answers when leanring
    /// </summary>
    public readonly struct Expected
    {
        /// <summary>
        /// Answers list
        /// </summary>
        public static readonly Matrix[] Answers = new Matrix[] {
                    new double[,] { {1},{0},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new double[,] { {0},{1},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new double[,] { {0},{0},{1},{0},{0},{0},{0},{0},{0},{0} },
                    new double[,] { {0},{0},{0},{1},{0},{0},{0},{0},{0},{0} },
                    new double[,] { {0},{0},{0},{0},{1},{0},{0},{0},{0},{0} },
                    new double[,] { {0},{0},{0},{0},{0},{1},{0},{0},{0},{0} },
                    new double[,] { {0},{0},{0},{0},{0},{0},{1},{0},{0},{0} },
                    new double[,] { {0},{0},{0},{0},{0},{0},{0},{1},{0},{0} },
                    new double[,] { {0},{0},{0},{0},{0},{0},{0},{0},{1},{0} },
                    new double[,] { {0},{0},{0},{0},{0},{0},{0},{0},{0},{1} },
                };
    }
}
