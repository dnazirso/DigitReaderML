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
        public static readonly List<Matrix> Answers = new List<Matrix> {
                    new float[,] { {1},{0},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{1},{0},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{1},{0},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{1},{0},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{1},{0},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{1},{0},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{1},{0},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{1},{0},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{0},{1},{0} },
                    new float[,] { {0},{0},{0},{0},{0},{0},{0},{0},{0},{1} },
                };
    }
}
