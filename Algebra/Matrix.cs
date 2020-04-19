using System.Linq;

namespace Algebra
{
    public struct Matrix
    {
        /// <summary>
        /// represent the matix itslef
        /// </summary>
        public readonly float[,] mat { get; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mat"></param>
        public Matrix(float[,] mat)
        {
            this.mat = mat;
        }

        /// <summary>
        /// Matrix A = new float[,]{};
        /// </summary>
        /// <param name="mat">a float[,]</param>
        public static implicit operator Matrix(float[,] mat) => new Matrix(mat);

        /// <summary>
        /// float[,] A = (Matrix)B;
        /// </summary>
        /// <param name="mat">a Matrix</param>
        public static explicit operator float[,](Matrix A) => A.mat;

        /// <summary>
        /// Transpose a matrix
        /// </summary>
        /// <param name="A">matrix</param>
        /// <returns>a transposed matrix</returns>
        public Matrix Transpose()
        {
            int dimAi = mat.GetLength(0);
            int dimAj = mat.GetLength(1);

            float[,] At = new float[dimAj, dimAi];

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    At[j, i] = mat[i, j];
                }
            }

            return new Matrix(At);
        }

        /// <summary>
        /// Determines whether the matrises are considered equal
        /// </summary>
        /// <param name="obj">The second matrix to compare</param>
        /// <returns>true if the matrises are considered equal; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj is Matrix)
            {
                float[,] f = ((Matrix)obj).mat;

                float[,] mat = this.mat;

                bool isEqual = mat.Rank == f.Rank
                    && Enumerable.Range(0, mat.Rank).All(dimension => mat.GetLength(dimension) == f.GetLength(dimension))
                    && mat.Cast<float>().SequenceEqual(f.Cast<float>());

                return isEqual;
            }

            return false;
        }

        /// <summary>
        /// Get matrix hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => mat.GetHashCode();

        /// <summary>
        /// Stringify the matrix representation
        /// </summary>
        /// <returns>a string representation of the matrix</returns>
        public override string ToString() => mat.ToString();

        /// <summary>
        /// Basic check of equality
        /// </summary>
        /// <param name="A">first matrix</param>
        /// <param name="B">second matrix</param>
        /// <returns>true if the matrises are considered equal; otherwise, false</returns>
        public static bool operator ==(Matrix A, Matrix B) => A.Equals(B);

        /// <summary>
        /// Basic check of inequality
        /// </summary>
        /// <param name="A">first matrix</param>
        /// <param name="B">second matrix</param>
        /// <returns>true if the matrises are considered inequal; otherwise, false</returns>
        public static bool operator !=(Matrix A, Matrix B) => !A.Equals(B);

        /// <summary>
        /// Matrix mutiplication
        /// Note : A width must be of the length than B height
        /// </summary>
        /// <param name="A">matrix A</param>
        /// <param name="B">matrix B</param>
        /// <returns>a square matrix result</returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            var dimAi = A.mat.GetLength(0);
            var dimAj = A.mat.GetLength(1);

            var dimBi = B.mat.GetLength(0);
            var dimBj = B.mat.GetLength(1);

            var dimCn = dimAj > dimBi ? dimBi : dimAj;

            var C = new float[dimAi, dimBj];

            float ComputeC(int i, int j, float cij = 0)
            {
                for (var n = 0; n < dimCn; n++)
                {
                    cij += A.mat[i, n] * B.mat[n, j];
                }
                return cij;
            }

            for (var i = 0; i < dimAi; i++)
            {
                for (var j = 0; j < dimBj; j++)
                {
                    C[i, j] += ComputeC(i, j);
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// Matrix mutiplication
        /// Note : A width must be of the length than B height
        /// </summary>
        /// <param name="A">matrix A</param>
        /// <param name="B">real B</param>
        /// <returns>a matrix result</returns>
        public static Matrix operator *(Matrix A, float B)
        {
            var dimAi = A.mat.GetLength(0);
            var dimAj = A.mat.GetLength(1);

            var C = new float[dimAi, dimAj];

            for (var i = 0; i < dimAi; i++)
            {
                for (var j = 0; j < dimAj; j++)
                {
                    C[i, j] = A.mat[i, j] * B;
                }
            }

            return new Matrix(C);
        }
        
        /// <summary>
        /// Matrix mutiplication
        /// Note : A width must be of the length than B height
        /// </summary>
        /// <param name="A">real A</param>
        /// <param name="B">matrix B</param>
        /// <returns>a matrix result</returns>
        public static Matrix operator *(float A, Matrix B)
        {
            return B * A;
        }

        /// <summary>
        /// Matrix addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">matrix A</param>
        /// <param name="B">matrix B</param>
        /// <returns>a matrix af same dimension</returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            var dimi = A.mat.GetLength(0);
            var dimj = A.mat.GetLength(1);

            var C = new float[dimi, dimj];

            for (var i = 0; i < dimi; i++)
            {
                for (var j = 0; j < dimj; j++)
                {
                    C[i, j] = A.mat[i, j] + B.mat[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// Matrix addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">matrix A</param>
        /// <param name="B">matrix B</param>
        /// <returns>a matrix af same dimension</returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            var dimi = A.mat.GetLength(0);
            var dimj = A.mat.GetLength(1);

            var C = new float[dimi, dimj];

            for (var i = 0; i < dimi; i++)
            {
                for (var j = 0; j < dimj; j++)
                {
                    C[i, j] = A.mat[i, j] - B.mat[i, j];
                }
            }

            return new Matrix(C);
        }
    }
}
