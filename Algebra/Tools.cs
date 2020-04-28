using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Algebra
{
    /// <summary>
    /// Thread safe random
    /// </summary>
    public static class ThreadSafeRandom
    {
        /// <summary>
        /// local instance
        /// </summary>
        [ThreadStatic] private static Random Local;

        /// <summary>
        /// thread safe random getter
        /// </summary>
        public static Random ThisThreadsRandom
        {
            get
            {
                return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
            }
        }

        /// <summary>
        /// Creates a Random double under the normal distribution probability law (Laplace–Gauss)
        /// </summary>
        /// <param name="mean">mean of the Gaussian curve (generaly 0)</param>
        /// <param name="scale">scale parameter of the normal distribution (generaly x²=1)</param>
        /// <returns>a normal random double</returns>
        public static double NormalRand(int mean = 0, int scale = 1)
        {
            double random1 = 1.0 - ThisThreadsRandom.NextDouble();
            double random2 = 1.0 - ThisThreadsRandom.NextDouble();
            
            return mean + scale * Math.Sqrt(-2.0 * Math.Log(random1)) * Math.Sin(2.0 * Math.PI * random2);
        }

    }

    /// <summary>
    /// Method extensions
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// Shuffle the items of a list
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">list</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Reorganise a list by chunks of smaller lists
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="source">Source list</param>
        /// <param name="chunkSize">Chunk size</param>
        /// <returns></returns>
        public static List<List<T>> ChunkBy<T>(this IList<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
    }
}
