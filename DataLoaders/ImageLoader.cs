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
    }
}
