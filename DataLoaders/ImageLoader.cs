using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace DataLoaders
{
    public class ImageLoader : IDataLoader
    {
        public float[] Load(string path)
        {
            string exactPath = Path.GetFullPath(path);

            Bitmap img = new Bitmap(exactPath);
            List<float> Brightnesses = new List<float>();

            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    Brightnesses.Add(img.GetPixel(i, j).GetBrightness());
                }
            }

            return Brightnesses.ToArray();
        }
    }
}
