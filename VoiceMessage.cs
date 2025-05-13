using System; // Importing the System namespace for basic functionalities
using System.IO;
using System.Media; // Importing the System.Media namespace to handle sound playback

namespace ChatBotCyberSecurityApp // Defining a namespace for the application
{
    public class VoiceMessage // Declaring an internal class named VoiceMessage
    {
        public VoiceMessage() { 
        // Get the base directory of the current application domain
        string project_location = AppDomain.CurrentDomain.BaseDirectory;

        // Output the project location to the console
        Console.WriteLine(project_location);

            // Update the project path by removing "bin\\Debug\\" from the base directory
            string updated_path = project_location.Replace("bin\\Debug\\", "");

        // Combine the updated path with the filename of the WAV file
        string full_path = Path.Combine(updated_path, "Greeting Message.wav");

        // Call the method to play the WAV file using the full path
        Play_wav(full_path);
        }

        // Method to play a WAV file given its full path
        private void Play_wav(string full_path)
        {
            try
            {
                // Create a SoundPlayer instance to play the sound file
                using (SoundPlayer player = new SoundPlayer(full_path))
                {
                    // Play the sound synchronously
                    player.PlaySync();
                }
            }
            catch (Exception error)
            {
                // Output any error messages to the console
                Console.WriteLine(error.Message);
            }
        }
    }
}
