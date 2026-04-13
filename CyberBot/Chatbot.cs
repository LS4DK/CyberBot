using System;
using System.Threading;
using System.Text.RegularExpressions;
using System.Linq;

public class Chatbot
{
    static string lastTopic = "";

    static void TypeText(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(20);
        }
        Console.WriteLine();
    }

    static void Speak(string f, string p, string ai, string c, string personality)
    {
        if (personality == "friendly") TypeText(f);
        else if (personality == "professional") TypeText(p);
        else if (personality == "ai") TypeText(ai);
        else TypeText(c);
    }

    static void Suggest(string topic)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (topic == "password")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- How long should a password be?");
            Console.WriteLine("- Why are strong passwords important?");
            Console.WriteLine("- What makes a password secure?");
        }
        else if (topic == "phishing")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- What is phishing?");
            Console.WriteLine("- How can I avoid phishing?");
            Console.WriteLine("- Why is phishing dangerous?");
        }
        else if (topic == "malware")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- How can I protect against malware?");
            Console.WriteLine("- What does malware do?");
        }
        else if (topic == "vpn")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- What is a VPN?");
            Console.WriteLine("- How does a VPN protect me?");
        }
        else if (topic == "2fa")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- What is 2FA?");
            Console.WriteLine("- Why should I use 2FA?");
        }
        else if (topic == "encryption")
        {
            Console.WriteLine("\nTry asking:");
            Console.WriteLine("- What is encryption?");
            Console.WriteLine("- How does encryption work?");
        }

        Console.ResetColor();
    }

    public static void Respond(string input, string name, string personality)
    {
        string normalized = (input ?? string.Empty).ToLowerInvariant();

        // Tokenize into whole words to avoid accidental substring matches
        var tokens = Regex.Split(normalized, "\\W+")
                          .Where(s => !string.IsNullOrEmpty(s))
                          .ToArray();

        bool isHow = tokens.Contains("how");
        bool isWhy = tokens.Contains("why");
        bool isWhat = tokens.Contains("what");
        bool isWhen = tokens.Contains("when");

        // REQUIRED QUESTIONS
        if (normalized.Contains("how are you"))
        {
            Speak(
                $"I'm doing great {name}! Ready to help!",
                "I am functioning optimally.",
                "System status: optimal.",
                "All good",
                personality
            );
            return;
        }

        if (normalized.Contains("purpose"))
        {
            Speak(
                "I help you stay safe online ",
                "My purpose is cybersecurity awareness.",
                "Primary function: user protection.",
                "I help you stay safe ",
                personality
            );
            return;
        }

        if (normalized.Contains("cybersecurity"))
        {
            Speak(
                "Cybersecurity protects your data and systems.",
                "Cybersecurity protects systems, networks, and data by preventing unauthorized access and attacks.",
                "Security protocol engaged.",
                "It helps keep your online accounts and devices safe.",
                personality
            );
            return;
        }

        // GREETING 
        // Check for greetings as whole words so words like "which" don't trigger it.
        if (tokens.Contains("hello") || tokens.Contains("hi") || tokens.Contains("hey"))
        {
            Speak(
                $"Hey {name}! What can I help you with?",
                $"Hello {name}. How may I assist you today?",
                $"Greetings {name}. Awaiting input.",
                $"Yo {name}! What can I do for you?",
                personality
            );
            return;
        }

        // TOPIC MEMORY
        string detectedTopic = "";

        if (normalized.Contains("password")) detectedTopic = "password";
        else if (normalized.Contains("phishing")) detectedTopic = "phishing";
        else if (normalized.Contains("malware")) detectedTopic = "malware";
        else if (normalized.Contains("vpn")) detectedTopic = "vpn";
        else if (normalized.Contains("2fa") || normalized.Contains("two factor") || normalized.Contains("two-factor")) detectedTopic = "2fa";
        else if (normalized.Contains("firewall")) detectedTopic = "firewall";
        else if (normalized.Contains("encryption")) detectedTopic = "encryption";
        else if (normalized.Contains("privacy")) detectedTopic = "privacy";
        else if (normalized.Contains("scam")) detectedTopic = "scam";

        // Detect whether the user is referencing the previous topic (e.g., "tell me more about that", "can you explain it?")
        var referenceWords = new string[] { "that", "this", "it", "more", "again", "same", "another", "details", "continue", "repeat" };
        bool referencesPrevious = tokens.Intersect(referenceWords).Any();

        string topic = "";

        if (!string.IsNullOrEmpty(detectedTopic))
        {
            topic = detectedTopic;
            lastTopic = topic; // update memory only when we detect a concrete topic in the input
        }
        else if (referencesPrevious && !string.IsNullOrEmpty(lastTopic))
        {
            // User explicitly referenced the previous topic, continue that topic
            topic = lastTopic;
        }
        else if (string.IsNullOrEmpty(lastTopic))
        {
            // No topic mentioned and no prior topic remembered
            TypeText("Please ask about a topic like passwords, phishing, or malware first.");
            return;
        }
        else
        {
            // There is a prior topic, but the user asked something else unrelated.
            // Use a sentinel so we fall through to the final default response instead of repeating.
            topic = "__unknown__";
        }

        // PASSWORDS
        if (topic == "password")
        {
            if (isHow)
                Speak("Use uppercase, lowercase, numbers and symbols ",
                      "Use a mix of character types, aim for at least 12 characters or a passphrase to increase strength.",
                      "Complex password generation recommended: long passphrases reduce guessability.",
                      "Mix character types and length ",
                      personality);
            else if (isWhy)
                Speak("It protects your account from unauthorized access by making guessing and brute force attacks harder.",
                      "Prevents unauthorized access and reduces the chance of account takeover.",
                      "Security enhanced.",
                      "Reduces compromise risk ",
                      personality);
            else if (isWhat)
                Speak("A password is a secret used to verify your identity when logging in.",
                      "A password is an authentication secret used to confirm a user's identity.",
                      "Authentication key recognized.",
                      "A credential for access ",
                      personality);
            else if (isWhen)
                Speak("Change passwords if you suspect a breach or at regular intervals; avoid reusing passwords across sites.",
                      "Passwords should be updated when compromised and rotated for critical accounts.",
                      "Security update required.",
                      "Replace compromised credentials ",
                      personality);
            else
            {
                Speak("Passwords are important and protect personal and financial data.",
                      "Passwords secure accounts and protect personal and financial data.",
                      "Strong passwords are a basic security control to protect accounts.",
                      "Use strong, unique passwords",
                      personality);
                Suggest("password");
            }
        }

        // PHISHING
        else if (topic == "phishing")
        {
            if (isHow)
                Speak("Avoid clicking suspicious links and attachments.",
                      "Verify sender addresses, hover links to see targets, and do not open unexpected attachments.",
                      "Verify sources before clicking and confirm requests out-of-band when in doubt.",
                      "Be cautious with links and attachments",
                      personality);
            else if (isWhy)
                Speak("Phishing tricks you into revealing credentials or personal data, often leading to identity theft or account compromise.",
                      "Leads to data theft and potential financial loss.",
                      "Security breach risk.",
                      "Can lead to data and financial loss ",
                      personality);
            else if (isWhat)
                Speak("Phishing is a fraudulent attempt to obtain sensitive information by impersonating trusted parties.",
                      "Phishing is a social engineering attack aimed at stealing credentials or delivering malware.",
                      "Threat detected.",
                      "A deceptive attack ",
                      personality);
            else
            {
                Speak("Phishing is dangerous and can lead to account takeover or malware infections.",
                      "Phishing is a common threat; always verify unexpected requests.",
                      "Threat level high.",
                      "Exercise caution ",
                      personality);
                Suggest("phishing");
            }
        }

        // MALWARE
        else if (topic == "malware")
        {
            if (isHow)
                Speak("Install antivirus and avoid unknown downloads ",
                      "Use protection software.",
                      "Defense protocol active.",
                      "Get protection ",
                      personality);
            else if (isWhy)
                Speak("It can steal data and damage files ",
                      "Malware can lead to data loss and compromise.",
                      "Threat escalation detected.",
                      "Can mess up your computer ",
                      personality);
            else if (isWhat)
                Speak("Malware is harmful software ",
                      "Malware is software designed to harm systems.",
                      "Malicious payload detected.",
                      "Bad software ",
                      personality);
            else if (isWhen)
                Speak("When you download unknown files or click links ",
                      "Often introduced via downloads or malicious links.",
                      "Infection vector identified.",
                      "When you install sketchy stuff ",
                      personality);
            else
            {
                Speak("Malware can damage files, steal information, and disrupt systems.",
                      "Malware is harmful software including viruses, trojans, and ransomware.",
                      "Threat detected.",
                      "Can compromise your system ",
                      personality);
                Suggest("malware");
            }
        }

        // VPN
        else if (topic == "vpn")
        {
            if (isHow)
                Speak("Use a reputable VPN app and connect before using public Wi-Fi ",
                      "Connect through a trusted VPN provider to encrypt traffic.",
                      "Tunnel established.",
                      "Turn it on before Wi-Fi ",
                      personality);
            else if (isWhy)
                Speak("It hides your activity on public networks ",
                      "A VPN protects your privacy and can prevent eavesdropping.",
                      "Privacy layer active.",
                      "Keeps your browsing private ",
                      personality);
            else if (isWhat)
                Speak("A VPN creates an encrypted tunnel for your data ",
                      "A VPN encrypts and routes your traffic through a secure server.",
                      "Encryption protocol active.",
                      "Private connection ",
                      personality);
            else
            {
                Speak("A VPN encrypts your network traffic and can protect you on untrusted networks.",
                      "A VPN creates an encrypted tunnel between your device and a remote server, hiding your activity on local networks.",
                      "Encryption protocol active.",
                      "Use a trusted VPN provider ",
                      personality);
                Suggest("vpn");
            }
        }

        // 2FA
        else if (topic == "2fa")
        {
            if (isHow)
                Speak("Enable it in your account settings and use an authenticator app ",
                      "Set up an authenticator or SMS-based second factor in settings.",
                      "Second factor required.",
                      "Turn it on in settings ",
                      personality);
            else if (isWhy)
                Speak("It stops attackers who have your password ",
                      "Adds a second verification step to reduce account takeover risk.",
                      "Authentication hardened.",
                      "Stops attackers ",
                      personality);
            else if (isWhat)
                Speak("2FA is an extra step to verify you ",
                      "Two-factor authentication requires two forms of proof to log in.",
                      "Multi-factor active.",
                      "Extra check ",
                      personality);
            else
            {
                Speak("Two-factor authentication adds a separate verification step, making account takeover harder.",
                      "Two-factor authentication improves security by requiring a second form of proof such as an app code or hardware key.",
                      "Multi-layer authentication active.",
                      "Enable 2FA for important accounts ",
                      personality);
                Suggest("2fa");
            }
        }

        // FIREWALL
        else if (topic == "firewall")
        {
            if (isHow)
                Speak("Keep it enabled and configure rules for apps ",
                      "Configure firewall rules and keep it active to filter traffic.",
                      "Filtering engaged.",
                      "Don’t turn it off ",
                      personality);
            else if (isWhy)
                Speak("It prevents unauthorized network access ",
                      "Blocks unwanted incoming or outgoing connections to protect systems.",
                      "Protection layer active.",
                      "Stops unwanted access ",
                      personality);
            else if (isWhat)
                Speak("A firewall filters network traffic ",
                      "A firewall monitors and controls network traffic based on rules.",
                      "Firewall active.",
                      "Network gatekeeper ",
                      personality);
            else
                Speak("A firewall monitors and controls incoming and outgoing network traffic based on security rules.",
                      "A firewall filters traffic to block unauthorized access and protect services.",
                      "Firewall active.",
                      "Helps defend the network ",
                      personality);
        }

        // ENCRYPTION
        else if (topic == "encryption")
        {
            Speak("Encryption converts data into a form that is unreadable without a key, protecting confidentiality.",
                  "Encryption secures information in transit and at rest using algorithms and keys.",
                  "Encryption protocol active.",
                  "Used to protect data ",
                  personality);
            Suggest("encryption");
        }

        // PRIVACY
        else if (topic == "privacy")
        {
            if (isHow)
                Speak("Limit what you share and check app permissions ",
                      "Review privacy settings and share only necessary data.",
                      "Privacy measures recommended.",
                      "Don’t give away everything ",
                      personality);
            else if (isWhy)
                Speak("Personal data can be abused ",
                      "Protecting privacy reduces identity theft and profiling risks.",
                      "Privacy risk noted.",
                      "Keeps you safe ",
                      personality);
            else
                Speak("Protect your personal information by limiting what you share and reviewing privacy settings.",
                      "Maintain online privacy by using strong passwords, 2FA, and careful sharing practices.",
                      "Privacy protocol active.",
                      "Be cautious with personal data ",
                      personality);
        }

        // SCAMS
        else if (topic == "scam")
        {
            if (isHow)
                Speak("Verify requests and never give credentials over email ",
                      "Be skeptical, verify identities and do not share sensitive info.",
                      "Fraud avoidance active.",
                      "Check first ",
                      personality);
            else if (isWhy)
                Speak("They take money or personal info ",
                      "Scams are designed to steal money or data from victims.",
                      "Scam risk high.",
                      "They want your info or money ",
                      personality);
            else
                Speak("Scams deceive people into giving up money or information; always verify requests and offers.",
                      "Scams often aim to steal money or personal data through social engineering.",
                      "Scam detected.",
                      "Exercise caution and verify claims ",
                      personality);
        }

        // ❌ DEFAULT
        else
        {
            TypeText("I'm sorry I cannot answer that question for you since I am still in the development phase.");
        }
    }
}