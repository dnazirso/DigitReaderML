using System;

namespace DataLoaders
{
    public interface IDataLoader : IDisposable
    {
        float[,] Load(string path);
    }
}