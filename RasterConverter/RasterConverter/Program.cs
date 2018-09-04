using System;
using System.Drawing;
using System.IO;

namespace RasterConverter
{
    class Program
    {
        static void Log(string msg)
        {
            Console.WriteLine(msg);
        }

        static void LogError(string msg)
        {
            Console.WriteLine("[ERROR] " + msg);
        }

        static void Main(string[] args)
        {
            // If there are no args, throw error and quit 
            if (args.Length == 0)
            {
                Log("RasterConverter - convert images to RTF files for RAST3R\nUsage:\nraster <path to file> [color depth, default 4] [base size, default 16]");
                return;
            }

            string path = args[0];

            // Check if file is a valid and supported image
            if (!File.Exists(path))
            {
                LogError("File not found!");
                return;
            }

            int colorDepth = 4;
            if (args.Length >= 2)
            {
                if (!int.TryParse(args[1], out colorDepth) || colorDepth < 1 || colorDepth > 0x7F)
                {
                    LogError("Invalid color depth! Please enter a number between 1 and 127");
                    return;
                }
            }

            int baseSize = 16;
            if (args.Length >= 3)
            {
                if (!int.TryParse(args[2], out baseSize) || baseSize < 1)
                {
                    LogError("Invalid base size! Please enter a number higher than 0");
                    return;
                }
            }

            Image image;
            try
            {
                image = Image.FromFile(path);
            }
            catch (OutOfMemoryException)
            {
                LogError("This file is not supported or corrupted! Please provide a valid image file");
                return;
            }

            Size bestSize = Useful.BestSize(image.Size, baseSize);

            int width = bestSize.Width;
            int height = bestSize.Height;

            Size newSize = new Size(width, height);
            Bitmap resized = ImageReader.ResizeImage(image, width, height, image.RawFormat);

            byte[] brightnessMap = ImageAnalysis.GetBrightnessMap(resized);

            try
            {
                brightnessMap = brightnessMap.SquishQuantize(colorDepth, true);
            }
            catch (DivideByZeroException)
            {
                LogError("Something went wrong! Try a lower color depth");
                return;
            }

            string toSave = String.Format("{0}\n{1}\n", width, height);
            foreach (byte b in brightnessMap)
            {
                toSave += b.ToString() + "\n";
            }
            
            File.WriteAllText(Path.ChangeExtension(path, "rtf"), toSave);

            string formatter = String.Format("X{0}", Math.Floor(Math.Log10(colorDepth - 1) / Math.Log10(16)) + 1);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(brightnessMap[j + i * width].ToString(formatter));
                }
                Console.WriteLine();
            }
        }
    }
}
