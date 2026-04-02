using System;

AudioPlayer.PlayGreeting();

// Title
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(@"
  _____       _               
 / ____|     | |              
| |     _   _| |__   ___ _ __ 
| |    | | | | '_ \ / _ \ '__|
| |____| |_| | |_) |  __/ |   
 \_____|\__, |_.__/ \___|_|   
         __/ |                
        |___/                 
");
Console.ResetColor();

Console.WriteLine("Welcome to the Cybersecurity Awareness Bot!\n");

// Get name
Console.Write("Enter your name: ");
string name = Console.ReadLine();

while (string.IsNullOrWhiteSpace(name))
{
    Console.Write("Name cannot be empty, try again: ");
    name = Console.ReadLine();
}

Console.WriteLine($"\nWelcome, {name}! Ask me anything about cybersecurity.");

// Chat loop
while (true)
{
    Console.Write("\nAsk a question (type 'exit' to quit): ");
    string input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("Please enter something.");
        continue;
    }

    if (input.ToLower() == "exit")
    {
        Console.WriteLine("Goodbye! Stay safe online 👋");
        break;
    }

    // Typing effect for thinking
    Console.ForegroundColor = ConsoleColor.Yellow;
    TypeText("Bot is thinking...");
    System.Threading.Thread.Sleep(300);
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.DarkMagenta; // dark purple
    Chatbot.Respond(input, name);
    Console.ResetColor();
}


// Typing effect method
void TypeText(string text)
{
    foreach (char c in text)
    {
        Console.Write(c);
        System.Threading.Thread.Sleep(20);
    }
    Console.WriteLine();
}