using System;
using System.Threading;

public class Chatbot
{
    static void TypeText(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(20);
        }
        Console.WriteLine();
    }

    //Improved Chatbot responses
    // Add comment
    // Changed the text
    public static void Respond(string input, string name)
    {
        input = input.ToLower();

        if (input.Contains("how are you"))
        {
            TypeText($"I'm doing great, {name}! 😄 Ready to keep you safe online.");
        }
        else if (input.Contains("purpose"))
        {
            TypeText("My purpose is to educate users about cybersecurity and safe online practices.");
        }
        else if (input.Contains("password"))
        {
            TypeText("Use strong passwords with uppercase, lowercase, numbers, and symbols. Avoid using personal info.");
        }
        else if (input.Contains("phishing"))
        {
            TypeText("Phishing is when scammers trick you into giving personal info through fake emails or websites. Always verify links.");
        }
        else if (input.Contains("malware"))
        {
            TypeText("Malware is harmful software designed to damage your system or steal data.");
        }
        else if (input.Contains("vpn"))
        {
            TypeText("A VPN encrypts your internet connection, helping protect your privacy online.");
        }
        else if (input.Contains("2fa") || input.Contains("two factor"))
        {
            TypeText("Two-factor authentication adds an extra layer of security by requiring a second verification step.");
        }
        else if (input.Contains("safe browsing"))
        {
            TypeText("Always check for HTTPS in websites, avoid suspicious links, and keep your browser updated.");
        }
        else if (input.Contains("hacker") || input.Contains("hacking"))
        {
            TypeText("Hackers can exploit system weaknesses. Always keep software updated and avoid downloading unknown files.");
        }
        else if (input.Contains("privacy"))
        {
            TypeText("Protect your personal information online and avoid sharing sensitive data on public platforms.");
        }
        else if (input.Contains("hello") || input.Contains("hi"))
        {
            TypeText($"Hello {name}! 👋 How can I help you today?");
        }
        else
        {
            TypeText("I didn’t understand that 🤔 Try asking about passwords, phishing, malware, VPN, or 2FA.");
        }
    }
}