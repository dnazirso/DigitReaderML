using Algebra;
using System;

namespace DataLoaders
{
    public interface IDataLoader : IDisposable
    {
        Matrix Load(string path);
    }
}