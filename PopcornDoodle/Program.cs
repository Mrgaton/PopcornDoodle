using System;
using System.Windows.Forms;

namespace PopcornDoodle
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        ///
        [STAThread]
        private static void Main()
        {
            /*Bitmap inputImage = (Properties.Resources.google_frame_mask); // Replace 'YourImageName' with the actual resource name

            Bitmap outputImage = new Bitmap(inputImage.Width, inputImage.Height);

            // Loop through each pixel
            for (int y = 0; y < inputImage.Height; y++)
            {
                for (int x = 0; x < inputImage.Width; x++)
                {
                    Color pixelColor = inputImage.GetPixel(x, y);

                    if (pixelColor.R >= 135 && pixelColor.G >= 35)
                    {
                        // Write black on the output image
                        outputImage.SetPixel(x, y, Color.FromArgb(139, 38,27));
                    }
                    else
                    {
                        // Keep the pixel transparent
                        outputImage.SetPixel(x, y, Color.Transparent);
                    }
                }
            }

            outputImage.Save("google_frame_mask_cleaned.png", ImageFormat.Png);*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Popcorn());
        }
    }
}