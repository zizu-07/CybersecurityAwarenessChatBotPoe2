using System; 
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq; 
using System.Text; 
using System.Threading.Tasks; 

namespace ChatBotCyberSecurityApp // Defining a namespace for the application
{
    public class AsciiTextImage // Declaring an internal class named AsciiTextImage
    {
        public AsciiTextImage(){ // Constructor for the AsciiTextImage class
// Get the full path of the application
            string paths = AppDomain.CurrentDomain.BaseDirectory;

        // Replace the "bin\\Debug\\" part of the path to get the root directory
        string new_path = paths.Replace("bin\\Debug\\", "");

        // Combine the new path with the logo image file name
        string full_path = Path.Combine(new_path, "logo.jpeg");
        Console.WriteLine(full_path); // Output the full path to the console

            // Load the logo image from the specified path
            Bitmap Logo = new Bitmap(full_path);
        // Resize the logo image to a specified width and height
        Logo = new Bitmap(Logo, new Size(150, 110));

            // Loop through each pixel in the height of the logo
            for (int height = 0; height<Logo.Height; height++)
            {
                // Loop through each pixel in the width of the logo
                for (int width = 0; width<Logo.Width; width++)
                {
                    // Get the color of the current pixel
                    Color pixelColor = Logo.GetPixel(width, height);
        // Calculate the grayscale value of the pixel
        int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
        // Determine the ASCII character based on the grayscale value
        char asciiChar = gray > 200 ? '.' : gray > 150 ? '*' : gray > 100 ? 'o' : gray > 50 ? '#' : '@';
        Console.Write(asciiChar); // Output the ASCII character to the console
                }
    Console.WriteLine(); // Move to the next line after finishing a row of pixels
            }
        }
    }
}

