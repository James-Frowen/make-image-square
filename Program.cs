using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MakeImageSquare
{
    internal class Program
    {
        private const int size = 1024;

        private static void Main(string[] args)
        {
            var path = args[0];
            using (var img = (Bitmap)Image.FromFile(path))
            {
                using (var output = new Bitmap(size, size, PixelFormat.Format32bppArgb))
                {
                    var width = img.Width;
                    var height = img.Height;

                    var offsetX = (width - size) / 2;
                    var offsetY = (height - size) / 2;

                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            var InX = x + offsetX;
                            var InY = y + offsetY;
                            Color c;
                            if (InX < 0 || InX >= width || InY < 0 || InY >= height)
                            {
                                c = Color.Transparent;
                            }
                            else
                            {
                                c = img.GetPixel(InX, InY);
                            }

                            output.SetPixel(x, y, c);
                        }
                    }

                    var folder = Path.GetDirectoryName(path);
                    var withoutExtension = Path.GetFileNameWithoutExtension(path);
                    var newPath = Path.Combine(folder, withoutExtension + "_Square.png");
                    output.Save(newPath);
                }
            }
        }
    }
}
