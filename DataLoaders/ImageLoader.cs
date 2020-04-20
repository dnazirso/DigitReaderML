using System;
using System.Drawing;
using System.IO;

namespace DataLoaders
{
    public class ImageLoader : IDataLoader
    {
        public float[,] Load(string path)
        {
            string exactPath = Path.GetFullPath(path);

            Bitmap img = new Bitmap(exactPath);
            float[,] Brightnesses = new float[img.Width * img.Height, 1];

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

        private bool disposed = false;

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
