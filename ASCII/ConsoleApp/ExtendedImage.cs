using System;
using System.Drawing;
using System.IO;

namespace AdvancedAscii.ConsoleApp
{
    public class ExtendedImage
    {
        private const int LASTBYTE = 0xFF;
        private const int BYTE = 8;
        private const int TWOBYTES = 16;
        private readonly Bitmap image;
        private readonly char[] charsByDarkness;
        private int rgbValue;

        public int Width
        {
            get { return image.Width; }
        }

        public int Height
        {
            get { return image.Height; }
        }

        public static ExtendedImage CreateImage(string fileName)
        {
            return new ExtendedImage(fileName);
        }

        private ExtendedImage(string fileName)
        {
            image = LoadImageFromFile(fileName);
            charsByDarkness = new char[] { '#', '@', 'X', 'L', 'I', ':', '.', ' ' };
        }

        public int GetIntensity(Point point)
        {
            int rgbValue = this.GetRgbValue(point);
            return GetRed() + GetBlue() + GetGreen();
        }

        public char[,] ToAscii()
        {
            int max = 0;
            int min = 255 * 3;
            int stepY = image.Height / 45;
            int stepX = image.Width / 150;
            for (int y = 0; y < image.Height; y += stepY)
            {
                for (int x = 0; x < image.Width; x += stepX)
                {
                    int sum = 0;
                    for (int avgy = 0; avgy < stepY; avgy++)
                    {
                        for (int avgx = 0; avgx < stepX; avgx++)
                        {
                            sum += GetIntensity(new Point(x, y));
                        }
                    }

                    sum = sum / stepY / stepX;

                    if (max < sum)
                    {
                        max = sum;
                    }

                    if (min > sum)
                    {
                        min = sum;
                    }
                }
            }

            for (int y = 0; y < image.Height - stepY; y += stepY)
            {
                for (int x = 0; x < image.Width - stepX; x += stepX)
                {
                    int sum = 0;
                    for (int avgy = 0; avgy < stepY; avgy++)
                    {
                        for (int avgx = 0; avgx < stepX; avgx++)
                        {
                            sum += GetIntensity(new Point(x, y));
                        }
                    }

                    sum = sum / stepY / stepX;
                    Console.Write(charsByDarkness[(sum - min) * charsByDarkness.Length / (max - min + 1)]);
                }

                Console.WriteLine();
            }

            Console.ReadLine();
        }

        private int GetRed()
        {
            return (rgbValue >> TWOBYTES) & LASTBYTE;
        }

        private int GetGreen()
        {
            return (rgbValue >> BYTE) & LASTBYTE;
        }

        private int GetBlue()
        {
            return rgbValue & LASTBYTE;
        }

        private int GetRgbValue(Point point)
        {
            if (!IsPointInImage(point))
            {
                throw new ArgumentOutOfRangeException(point.ToString());
            }

            return image.GetPixel(point.X, point.Y).ToArgb();
        }

        private bool IsPointInImage(Point point)
        {
            bool isHorizontalCorrect = point.X >= 0 || point.X < Width;
            bool isVerticalCorrect = point.Y >= 0 || point.Y < Height;

            return isVerticalCorrect && isHorizontalCorrect;
        }

        private Bitmap LoadImageFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException($"File not found: {fileName}", nameof(fileName));
            }

            using (var image = Image.FromFile(fileName))
            {
                return new Bitmap(image);
            }
        }
    }
}