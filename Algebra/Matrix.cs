using Algebra.Exceptions;
using System.Linq;

namespace Algebra
{
    public struct Matrix
    {
        /// <summary>
        /// Represent the <see cref="Matrix"/> itslef
        /// </summary>
        public readonly float[,] mat { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mat"></param>
        public Matrix(float[,] mat)
        {
            this.mat = mat;
        }

        /// <summary>
        /// <see cref="Matrix"/> A = new float[,]{};
        /// </summary>
        /// <param name="mat">a float[,]</param>
        public static implicit operator Matrix(float[,] mat) => new Matrix(mat);

        /// <summary>
        /// float[,] A = (<see cref="Matrix"/>)B;
        /// </summary>
        /// <param name="mat">a Matrix</param>
        public static explicit operator float[,](Matrix A) => A.mat;

        /// <summary>
        /// Transpose a <see cref="Matrix"/>
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
        /// Determines whether the <see cref="Matrix"/>s are considered equal
        /// </summary>
        /// <param name="obj">The second <see cref="Matrix"/> to compare</param>
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
        /// Get the <see cref="Matrix"/> hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => mat.GetHashCode();

        /// <summary>
        /// ToString method simple override
        /// </summary>
        /// <returns>a string</returns>
        public override string ToString() => mat.ToString();

        /// <summary>
        /// Check equality of two <see cref="Matrix"/>s
        /// </summary>
        /// <param name="A">first <see cref="Matrix"/></param>
        /// <param name="B">second <see cref="Matrix"/></param>
        /// <returns>true if the <see cref="Matrix"/>s are considered equal; otherwise, false</returns>
        public static bool operator ==(Matrix A, Matrix B) => A.Equals(B);

        /// <summary>
        /// Basic check of inequality
        /// </summary>
        /// <param name="A">first <see cref="Matrix"/></param>
        /// <param name="B">second <see cref="Matrix"/></param>
        /// <returns>true if the <see cref="Matrix"/>s are considered inequal; otherwise, false</returns>
        public static bool operator !=(Matrix A, Matrix B) => !A.Equals(B);

        /// <summary>
        /// <see cref="Matrix"/> Hadamard product
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A and B</returns>
        public static Matrix operator %(Matrix A, Matrix B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            if (dimAi != dimBi || dimAj != dimBj)
            {
                throw new DifferentSizesException("A and B has to be of the same dimensions");
            }

            float[,] C = new float[dimAi, dimAj];

            for (int i = 0; i < dimAi; i++)
            {
                for (int j = 0; j < dimAj; j++)
                {
                    C[i, j] = A.mat[i, j] * B.mat[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// <see cref="Matrix"/> mutiplication
        /// Note : A width must be of the same length than B height
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a square <see cref="Matrix"/> result</returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            if (dimAj != dimBi)
            {
                throw new DifferentSizesException("A width must be of the same length than B height");
            }

            float[,] C = new float[dimAi, dimBj];

            float ComputeC(int i, int j, float cij = 0)
            {
                for (int n = 0; n < dimAj; n++)
                {
                    cij += A.mat[i, n] * B.mat[n, j];
                }
                return cij;
            }

            for (int i = 0; i < dimAi; i++)
            {
                for (int j = 0; j < dimBj; j++)
                {
                    C[i, j] += ComputeC(i, j);
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// <see cref="Matrix"/> mutiplication with a real number
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B">real B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A</returns>
        public static Matrix operator *(Matrix A, float B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            float[,] C = new float[dimAi, dimAj];

            for (int i = 0; i < dimAi; i++)
            {
                for (int j = 0; j < dimAj; j++)
                {
                    C[i, j] = A.mat[i, j] * B;
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// <see cref="Matrix"/> mutiplication
        /// </summary>
        /// <param name="A">real A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than B</returns>
        public static Matrix operator *(float A, Matrix B)
        {
            return B * A;
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A nad B</returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            if (dimAi != dimBi || dimAj != dimBj)
            {
                throw new DifferentSizesException("A and B has to be of the same dimensions");
            }

            float[,] C = new float[dimAi, dimAj];

            for (int i = 0; i < dimAi; i++)
            {
                for (int j = 0; j < dimAj; j++)
                {
                    C[i, j] = A.mat[i, j] + B.mat[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// <see cref="Matrix"/> substraction
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension</returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            if (dimAi != dimBi || dimAj != dimBj)
            {
                throw new DifferentSizesException("A and B has to be of the same dimensions");
            }

            float[,] C = new float[dimAi, dimAj];

            for (int i = 0; i < dimAi; i++)
            {
                for (int j = 0; j < dimAj; j++)
                {
                    C[i, j] = A.mat[i, j] - B.mat[i, j];
                }
            }

            return new Matrix(C);
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">a is a number representing the 1 <see cref="Matrix"/></param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A nad B</returns>
        public static Matrix operator +(float a, Matrix B)
        {
            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            float[,] A = new float[dimBi, dimBj];

            for (int i = 0; i < dimBi; i++)
            {
                for (int j = 0; j < dimBj; j++)
                {
                    A[i, j] = a;
                }
            }

            return A + B;
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">a is a number representing the Identity <see cref="Matrix"/></param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A nad B</returns>
        public static Matrix operator +(Matrix B, float a)
        {
            return a + B;
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">a is a number representing the 1 <see cref="Matrix"/></param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A nad B</returns>
        public static Matrix operator -(float a, Matrix B)
        {
            int dimBi = B.mat.GetLength(0);
            int dimBj = B.mat.GetLength(1);

            float[,] A = new float[dimBi, dimBj];

            for (int i = 0; i < dimBi; i++)
            {
                for (int j = 0; j < dimBj; j++)
                {
                    A[i, j] = a;
                }
            }

            return A - B;
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A">a is a number representing the Identity <see cref="Matrix"/></param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension than A nad B</returns>
        public static Matrix operator -(Matrix B, float a)
        {
            return a - B;
        }
    }
}
