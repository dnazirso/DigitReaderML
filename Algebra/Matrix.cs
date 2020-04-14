namespace Algebra
{
    public struct Matrix
    {
        /// <summary>
        /// represent the matix itslef
        /// </summary>
        private float[,] mat { get; set; }

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
        /// <param name="mat">float[,]</param>
        //public static implicit operator Matrix(float[,] mat) => new Matrix(mat);
        //public static explicit operator float[,](Matrix A) => A.mat;

        /// <summary>
        /// Transpose a matrix
        /// </summary>
        /// <param name="A">matrix</param>
        /// <returns>a transposed matrix</returns>
        public Matrix Transpose()
        {
            var dimAi = mat.GetLength(0);
            var dimAj = mat.GetLength(1);

            var At = new float[dimAj, dimAi];

            for (var i = 0; i < mat.GetLength(0); i++)
            {
                for (var j = 0; j < mat.GetLength(1); j++)
                {
                    At[i, j] = mat[j, i];
                }
            }

            return new Matrix(At);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Matrix))
            {
                return false;
            }

            var f = ((Matrix)obj).mat;

            var isEqual = mat.Equals(f);

            return isEqual;
        }

        public override int GetHashCode() => mat.GetHashCode();
        public override string ToString() => mat.ToString();
        public static bool operator ==(Matrix A, Matrix B) => A.mat == B.mat;
        public static bool operator !=(Matrix A, Matrix B) => A.mat != B.mat;

        /// <summary>
        /// Matrix mutiplication
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

            var imax = dimAi > dimBj ? dimBj : dimAi;
            var jmax = dimAj > dimBi ? dimBi : dimAj;

            var C = new float[imax, jmax];

            float ComputeC(int m, int p)
            {
                float cij = 0;
                for (var n = 0; n < jmax; n++)
                {
                    cij += A.mat[m, n] * B.mat[n, p];
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

            return new Matrix(C);
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
