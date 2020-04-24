using Algebra;
using System;

namespace DataLoaders
{
    /// <summary>
    /// Data loader interface
    /// </summary>
    public interface IDataLoader : IDisposable
    {
        /// <summary>
        /// Fetch and load the data
        /// </summary>
        /// <param name="path">path to the data</param>
        /// <returns>a Matrix</returns>
        Matrix Load(string path);
    }
}