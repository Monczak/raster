using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace RasterConverter
{
    class ImageReader
    {
        public static Bitmap ResizeImage(Image image, int width, int height, ImageFormat format)
        {
            Image newImage = image.GetThumbnailImage(width, height, null, IntPtr.Zero);
            MemoryStream result = new MemoryStream();
            newImage.Save(result, format);
            return new Bitmap(result);
        }

        public static byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }
    }
}
