using Algebra;

namespace NeuralNetwork
{
    /// <summary>
    /// Represent the <see cref="Data"/> structure
    /// </summary>
    public struct Data
    {
        /// <summary>
        /// <see cref="Data"/> Identifier, commonly its path
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The <see cref="Data"/> inputs itself
        /// </summary>
        public Matrix Inputs { get; set; }

        /// <summary>
        /// The expected answer from the <see cref="Network"/>
        /// </summary>
        public Matrix Expected { get; set; }
    }
}
