using Algebra;
using System;
using System.Drawing;
using System.IO;

namespace DataLoaders
{
    /// <summary>
    /// Loader specific for image
    /// </summary>
    public class ImageLoader : IDataLoader
    {
        /// <summary>
        /// Loads an image and transform its pixels into a bidimensional array
        /// of numbers from their brightness. Each one going from 0.0 to 1.0
        /// </summary>
        /// <param name="path">path to a picture</param>
        /// <returns>a bidimensional array of numbers</returns>
        public Matrix Load(string path)
        {
            string exactPath = Path.GetFullPath(path);

            Bitmap img = new Bitmap(exactPath);
            double[,] Brightnesses = new double[img.Width * img.Height, 1];

            int n = 0;

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Brightnesses[n, 0] = img.GetPixel(i, j).GetBrightness();
                    n++;
                }
            }

            return Brightnesses;
        }

        /// <summary>
        /// Indicate weither the loader is disposed or not
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Dispose the instance
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Set the state of the loader as diposed if not
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!disposed)
            {
                // Note disposing has been done.
                disposed = true;
            }
        }
    }
}
