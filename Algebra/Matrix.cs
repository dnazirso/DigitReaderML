using System.Linq;

namespace Algebra
{
    public struct Matrix
    {
        /// <summary>
        /// Represent the <see cref="Matrix"/> itslef
        /// </summary>
        public readonly double[,] mat { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mat"></param>
        public Matrix(double[,] mat)
        {
            this.mat = mat;
        }

        /// <summary>
        /// <see cref="Matrix"/> A = new double[,]{};
        /// </summary>
        /// <param name="mat">a double[,]</param>
        public static implicit operator Matrix(double[,] mat) => new Matrix(mat);

        /// <summary>
        /// double[,] A = (<see cref="Matrix"/>)B;
        /// </summary>
        /// <param name="mat">a Matrix</param>
        public static explicit operator double[,](Matrix A) => A.mat;

        /// <summary>
        /// Transpose a <see cref="Matrix"/>
        /// </summary>
        /// <param name="A">matrix</param>
        /// <returns>a transposed matrix</returns>
        public Matrix Transpose()
        {
            int dimAi = mat.GetLength(0);
            int dimAj = mat.GetLength(1);

            double[,] At = new double[dimAj, dimAi];

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
                double[,] f = ((Matrix)obj).mat;

                double[,] mat = this.mat;

                bool isEqual = mat.Rank == f.Rank
                    && Enumerable.Range(0, mat.Rank).All(dimension => mat.GetLength(dimension) == f.GetLength(dimension))
                    && mat.Cast<double>().SequenceEqual(f.Cast<double>());

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

            int dimCn = dimAj > dimBi ? dimBi : dimAj;

            double[,] C = new double[dimAi, dimBj];

            double ComputeC(int i, int j, double cij = 0)
            {
                for (int n = 0; n < dimCn; n++)
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
        public static Matrix operator *(Matrix A, double B)
        {
            int dimAi = A.mat.GetLength(0);
            int dimAj = A.mat.GetLength(1);

            double[,] C = new double[dimAi, dimAj];

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
        public static Matrix operator *(double A, Matrix B)
        {
            return B * A;
        }

        /// <summary>
        /// <see cref="Matrix"/> addition
        /// Note : A and B has to be of the same dimensions
        /// </summary>
        /// <param name="A"><see cref="Matrix"/> A</param>
        /// <param name="B"><see cref="Matrix"/> B</param>
        /// <returns>a <see cref="Matrix"/> of same dimension</returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            int dimi = A.mat.GetLength(0);
            int dimj = A.mat.GetLength(1);

            double[,] C = new double[dimi, dimj];

            for (int i = 0; i < dimi; i++)
            {
                for (int j = 0; j < dimj; j++)
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
            int dimi = A.mat.GetLength(0);
            int dimj = A.mat.GetLength(1);

            double[,] C = new double[dimi, dimj];

            for (int i = 0; i < dimi; i++)
            {
                for (int j = 0; j < dimj; j++)
                {
                    C[i, j] = A.mat[i, j] - B.mat[i, j];
                }
            }

            return new Matrix(C);
        }
    }
}
