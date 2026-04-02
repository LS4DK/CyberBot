using System;
using System.IO;
using System.Media;

public class AudioPlayer
{
    public static void PlayGreeting()
    {
        try
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

            Console.WriteLine("Looking for file at:");
            Console.WriteLine(path);

            if (!File.Exists(path))
            {
                Console.WriteLine("❌ File NOT found");
                return;
            }

            Console.WriteLine("✅ File found, playing audio...\n");

            SoundPlayer player = new SoundPlayer(path);
            player.PlaySync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
        }
    }
}
