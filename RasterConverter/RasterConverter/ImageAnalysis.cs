using System.Drawing;


namespace RasterConverter
{
    class ImageAnalysis
    {
        public static byte[] GetBrightnessMap(Bitmap bitmap)
        {
            int width = bitmap.Size.Width;
            int height = bitmap.Size.Height;

            byte[] result = new byte[width * height];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color pixel = bitmap.GetPixel(j, i);
                    byte brightness = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    result[j + i * width] = brightness;
                }
            }

            return result;
        }
    }
}
