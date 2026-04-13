using System;
using System.IO;
using System.Media;

public class AudioPlayer
{
    public static void PlayGreeting()
    {
        try
        {
            // Look in several likely locations so the app works whether the WAV
            // was copied to the output folder or is sitting in the project folder.
            string[] candidates = new string[] {
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav"),
                Path.Combine(Environment.CurrentDirectory, "greeting.wav")
            };

            // also check parent directories up to 3 levels (helps during debugging)
            string dir = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
            for (int i = 0; i < 3; i++)
            {
                dir = Path.GetDirectoryName(dir) ?? dir;
                if (string.IsNullOrEmpty(dir)) break;
                candidates = Append(candidates, Path.Combine(dir, "greeting.wav"));
            }

            Console.WriteLine("Looking for greeting.wav in these locations:");
            foreach (var c in candidates)
                Console.WriteLine(c);

            string found = null;
            foreach (var c in candidates)
            {
                if (File.Exists(c))
                {
                    found = c;
                    break;
                }
            }

            if (found == null)
            {
                Console.WriteLine("❌ greeting.wav not found. Add the file to the output folder and set 'Copy to Output Directory' -> 'Copy if newer'.");
                return;
            }

            Console.WriteLine("✅ File found, playing audio...\n");
            SoundPlayer player = new SoundPlayer(found);
            player.PlaySync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
        }
    }

    // small helper to add to array without bringing LINQ in
    private static string[] Append(string[] arr, string item)
    {
        var res = new string[arr.Length + 1];
        Array.Copy(arr, res, arr.Length);
        res[res.Length - 1] = item;
        return res;
    }
}
