using System;

AudioPlayer.PlayGreeting();

// 🔥 Creative ASCII
Console.ForegroundColor = ConsoleColor.DarkMagenta;
Console.WriteLine(@"
   ██████╗██╗   ██╗██████╗ ███████╗██████╗ 
  ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗
  ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝
  ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗
  ╚██████╗   ██║   ██████╔╝███████╗██║  ██║
   ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝

        CYBER SECURITY BOT
");
Console.ResetColor();

Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("\nWELCOME TO THE CYBER SECURITY AWARENESS BOT\n");
Console.ResetColor();

Console.WriteLine("Topics you can ask about:");
Console.WriteLine("- Passwords");
Console.WriteLine("- Phishing");
Console.WriteLine("- Malware");
Console.WriteLine("- VPN");
Console.WriteLine("- 2FA");
Console.WriteLine("- Firewalls");
Console.WriteLine("- Encryption");
Console.WriteLine("- Scams");
Console.WriteLine("- Privacy");

// Name
Console.Write("\nEnter your name: ");
string name = Console.ReadLine();

while (string.IsNullOrWhiteSpace(name))
{
    Console.Write("Name cannot be empty, try again: ");
    name = Console.ReadLine();
}

// Personality
Console.WriteLine("\nChoose chatbot personality:");
Console.WriteLine("1. Friendly");
Console.WriteLine("2. Professional");
Console.WriteLine("3. Futuristic AI");
Console.WriteLine("4. Casual");

Console.Write("Enter choice (1-4): ");
string choice = Console.ReadLine();

string personality = "friendly";

switch (choice)
{
    case "1": personality = "friendly"; break;
    case "2": personality = "professional"; break;
    case "3": personality = "ai"; break;
    case "4": personality = "casual"; break;
    default:
        Console.WriteLine("Invalid choice, defaulting to Friendly.");
        break;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\nWelcome, {name}! Ask me anything");
Console.ResetColor();

// Chat loop
while (true)
{
    Console.Write("\nYou: ");
    string input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Please enter something.");
        continue;
    }

    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Goodbye! Stay safe online");
        break;
    }

    Console.ForegroundColor = ConsoleColor.Yellow;
    TypeText("Bot is thinking...");
    System.Threading.Thread.Sleep(300);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.Write("Bot: ");
    Chatbot.Respond(input, name, personality);
    Console.ResetColor();
}

// Typing effect
void TypeText(string text)
{
    foreach (char c in text)
    {
        Console.Write(c);
        System.Threading.Thread.Sleep(20);
    }
    Console.WriteLine();
}