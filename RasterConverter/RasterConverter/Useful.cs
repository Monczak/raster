using System.Drawing;

namespace RasterConverter
{
    class Useful
    {
        public static Size BestSize(Size origSize, int desiredWidth)
        {
            float aspectRatio = (float)origSize.Height / (float)origSize.Width;
            float newHeight = desiredWidth * aspectRatio;

            return new Size(desiredWidth, (int)newHeight);
        }
    }
}
