namespace Algebra
{
    public static class Matrix
    {
        /// <summary>
        /// Transpose a matrix
        /// </summary>
        /// <param name="A">matrix</param>
        /// <returns>a transposed matrix</returns>
        public static float[,] Transpose(float[,] A)
        {
            var dimAi = A.GetLength(0);
            var dimAj = A.GetLength(1);

            var At = new float[dimAj, dimAi];

            for (var i = 0; i < A.GetLength(0); i++)
            {
                for (var j = 0; j < A.GetLength(1); j++)
                {
                    At[i, j] = A[j, i];
                }
            }

            return At;
        }

        /// <summary>
        /// Matrix mutiplication
        /// </summary>
        /// <param name="A">matrix A</param>
        /// <param name="B">matrix B</param>
        /// <returns>a square matrix result</returns>
        public static float[,] Dot(float[,] A, float[,] B)
        {
            var dimAi = A.GetLength(0);
            var dimAj = A.GetLength(1);

            var dimBi = B.GetLength(0);
            var dimBj = B.GetLength(1);

            var imax = dimAi > dimBj ? dimBj : dimAi;
            var jmax = dimAj > dimBi ? dimBi : dimAj;

            var C = new float[imax, jmax];

            float ComputeC(int m, int p)
            {
                float cij = 0;
                for (var n = 0; n < jmax; n++)
                {
                    cij += A[m, n] * B[n, p];
                }
                return cij;
            }

            for (var i = 0; i < imax; i++)
            {
                for (var j = 0; j < jmax; j++)
                {
                    C[i, j] += ComputeC(i, j);
                }
            }

            return C;
        }
    }
}
