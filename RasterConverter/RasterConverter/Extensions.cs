using System;
using System.Drawing;
using System.Linq;

namespace RasterConverter
{
    public static class Extensions
    {
        public static double Remap(this double value, double from1, double to1, double from2, double to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }

        public static Size Divide(this Size size, int divisor)
        {
            return new Size(size.Width / divisor, size.Height / divisor);
        }

        public static byte[] SquishQuantize(this byte[] data, int depth, bool invert = false)
        {
            byte max = data.Max();
            byte min = data.Min();
            
            byte[] quantizeLookup = new byte[256];

            for (int i = 0; i <= 0xFF; i++)
            {
                quantizeLookup[i] = (byte)Math.Floor((double)(i / (max / depth)));
            }

            byte[] result = new byte[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                result[i] = quantizeLookup[data[i]];
            }

            byte minQuantize = result.Min();
            byte maxQuantize = result.Max();

            for (int i = 0; i < data.Length; i++)
            {
                double toRemap = result[i];
                result[i] = (byte)toRemap.Remap(minQuantize, maxQuantize, invert ? (depth - 1) : 0, invert ? 0 : (depth - 1));
            }

            return result;
        }
    }
}
