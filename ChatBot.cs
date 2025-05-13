using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// This class represents a ChatBot designed to assist users with cybersecurity inquiries.
public class ChatBot
{
    // Stores the user's name
    private string userName = string.Empty;
    // Stores the user's current question
    private string userAsking = string.Empty;
    // Stores the current topic of conversation
    private string currentTopic = string.Empty;
    // Stores the user's favorite topic
    private string favoriteTopic = string.Empty;

    // Dictionary to hold keyword-based responses
    private readonly Dictionary<string, List<string>> keywordResponses;
    // Dictionary for basic responses to common queries
    private readonly Dictionary<string, string> basicResponses;
    // Dictionary to hold keywords related to user sentiment
    private readonly Dictionary<string, List<string>> sentimentKeywords;
    // Dictionary for dynamic responses based on user sentiment
    private readonly Dictionary<string, List<string>> sentimentDynamicResponses;

    // Random number generator for selecting responses
    private readonly Random random = new Random();

    // Constructor to initialize the ChatBot
    public ChatBot()
    {
        // Initialize dictionaries with case-insensitive string comparison
        keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        basicResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        sentimentKeywords = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        sentimentDynamicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        // Call method to set up responses
        InitializeResponses();
        // Start the chat interaction
        StartChat();
    }

    // Method to initialize various types of responses
    private void InitializeResponses()
    {
        // Add basic responses to the dictionary
        AddBasicResponses();
        // Add keyword-based responses to the dictionary
        AddKeywordResponses();
        // Add sentiment-based responses to the dictionary
        AddSentimentResponses();
    }

    // Method to start the chat with the user
    private void StartChat()
    {
        // Display the welcome banner
        DisplayBanner();
        // Request the user's name
        RequestUserName();
        // Prompt the user for assistance
        effectTyping($"Hey {userName}, how can I assist you today? (Type 'exit' to end the conversation)", ConsoleColor.Green);

        // Loop to keep the chat going until the user types 'exit'
        do
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(userName + ": ");
            Console.ForegroundColor = ConsoleColor.White;
            userAsking = Console.ReadLine()?.Trim();

            // Check if the user input is empty
            if (string.IsNullOrWhiteSpace(userAsking))
            {
                effectTyping("ChatBot: Please enter something, I can't help if you say nothing!", ConsoleColor.Red);
                continue; // Continue to the next iteration of the loop
            }

            // Get a response based on user input
            string response = GetResponse(userAsking);

            // If a valid response is found, display it
            if (!string.IsNullOrEmpty(response))
            {
                effectTyping(response, ConsoleColor.Blue);
            }
            else
            {
                // If no valid response is found, prompt the user to rephrase
                effectTyping("ChatBot: I'm not sure I understand. Can you try rephrasing or ask something related to cybersecurity?", ConsoleColor.DarkRed);
                Console.Beep(); // Sound an alert
            }

        } while (!userAsking.Equals("exit", StringComparison.OrdinalIgnoreCase)); // Exit condition for the loop

        // Farewell message when the chat ends
        effectTyping("ChatBot: Goodbye! Stay safe online.", ConsoleColor.Yellow);
    }

    // Method to add basic responses to the dictionary
    private void AddBasicResponses()
    {
        // Array of predefined responses
        var responses = new[]
        {
            ("hi", "Hello! How can I assist you with cybersecurity today?"),
            ("hello", "Hi! Feel free to ask me about cybersecurity."),
            ("hey", "Hey there! What cybersecurity topic can I help with?"),
            ("how are you", "I'm just a bot, but I'm always here to help you stay safe online!"),
            ("okay", "Anything else? If not Type 'EXIT' to end this conversation"),
            ("what is your purpose", "My purpose is to educate you on cybersecurity threats and safe online practices."),
            ("what can i ask you about", "You can ask me about password safety, phishing, scams, privacy, and more."),
            ("exit", "Bye, feel free to ask me anything related to cybersecurity!"),
            ("thank you", "You're welcome. Want to continue or end this conversation? Type 'EXIT'.")
            
        };

        // Populate the basic responses dictionary
        foreach (var (key, value) in responses)
            basicResponses[key] = "ChatBot: " + value; // Format response
    }

    // Method to add keyword-based responses to the dictionary
    private void AddKeywordResponses()
    {
        // Dictionary of topics and their corresponding tips
        var topics = new Dictionary<string, List<string>>
        {
            ["password"] = new List<string>
            {
                "Use strong, unique passwords for every account.",
                "Avoid using personal info like birthdays or names in passwords.",
                "Consider a password manager to help you manage passwords securely."
            },
            ["phishing"] = new List<string>
            {
                "Check the sender’s email carefully – scammers mimic real addresses.",
                "Don’t click on links from unknown sources.",
                "If it sounds too urgent or threatening, it might be phishing!"
            },
            ["scam"] = new List<string>
            {
                "Online scams often look real. Always verify before clicking.",
                "Never share personal info unless you're 100% sure it's safe.",
                "Watch out for fake job offers and investment deals!"
            },
            ["cybersecurity"] = new List<string>
            {
                "Cybersecurity protects your devices, data, and online accounts from hackers, viruses, and scams.",
                "To enhance cybersecurity, individuals and companies should focus on strong passwords, software updates, and awareness of phishing scams."
            },
            ["privacy"] = new List<string>
            {
                "Review your privacy settings on apps and social media regularly.",
                "Don’t overshare online. What you post can be used against you.",
                "Use privacy-focused tools like VPNs and encrypted messaging apps."
            },
            ["malware"] = new List<string>
            {
                "Avoid downloading software from unknown sites.",
                "Keep your antivirus and operating system updated.",
                "Scan USB drives before opening files."
            },
            ["network security"] = new List<string>
            {
                "Use secure Wi-Fi and avoid public networks for sensitive tasks.",
                "Enable firewalls and use encryption to protect your data.",
                "Change default router passwords immediately."
            }
        };

        // Populate the keyword responses dictionary
        foreach (var topic in topics)
            keywordResponses[topic.Key] = topic.Value; // Assign tips to each topic
    }

    // Method to add sentiment-based responses to the dictionary
    private void AddSentimentResponses()
    {
        // Define keywords associated with different sentiments
        sentimentKeywords["sad"] = new List<string> { "sad", "down", "depressed", "unhappy", "miserable" };
        sentimentKeywords["angry"] = new List<string> { "angry", "mad", "furious", "irritated", "annoyed" };
        sentimentKeywords["worried"] = new List<string> { "worried", "anxious", "nervous", "scared", "afraid" };
        sentimentKeywords["confused"] = new List<string> { "confused", "lost", "unsure", "puzzled" };
        sentimentKeywords["curious"] = new List<string> { "curious", "interested", "wondering", "inquisitive" };
        sentimentKeywords["happy"] = new List<string> { "happy", "good", "great", "excited", "joyful" };

        // Define dynamic responses for each sentiment
        sentimentDynamicResponses["sad"] = new List<string>
        {
            "I'm here for you. If you're feeling sad, it's okay to take a break.",
            "It’s tough feeling this way. Let’s talk about something that might lift your mood.",
            "Remember, you're not alone. I'm always here to support you — even as a bot."
        };
        sentimentDynamicResponses["angry"] = new List<string>
        {
            "Anger is valid — want to talk about what’s bothering you?",
            "Let's take a breath together. Cyber threats are frustrating, but manageable.",
            "Sounds like you're angry. Want some calming cybersecurity trivia?"
        };
        sentimentDynamicResponses["worried"] = new List<string>
        {
            "You're right to be cautious — the web can be risky, but knowledge is power.",
            "Don't worry. I'm here to help you understand how to stay safe online.",
            "Concern is a good first step to awareness. Let’s explore a topic together."
        };
        sentimentDynamicResponses["confused"] = new List<string>
        {
            "No problem — I can explain things step by step.",
            "Confusion is natural when learning something new. Which part can I clarify?",
            "Let’s simplify it together. Ask me anything you’d like cleared up."
        };
        sentimentDynamicResponses["curious"] = new List<string>
        {
            "Curiosity is your best ally in cybersecurity.",
            "Great! Curiosity leads to awareness. What do you want to dive into?",
            "I love that you're curious. Let me give you something interesting to explore."
        };
        sentimentDynamicResponses["happy"] = new List<string>
        {
            "That’s great to hear! Want to learn a quick tip while you're in a good mood?",
            "Awesome! Being happy makes learning easier. Let’s keep that energy going.",
            "Glad to hear you're feeling good. Let’s talk about something exciting in cybersecurity!"
        };
    }

    // Method to generate a response based on user input
    private string GetResponse(string input)
    {
        input = input.ToLower(); // Normalize input to lowercase

        // Check if the user is asking for their name
        if (input.Contains("name") && (input.Contains("my") || input.Contains("what")))
        {
            return !string.IsNullOrEmpty(userName)
                ? $"ChatBot: You told me your name is {userName}." // Respond with the user's name
                : "ChatBot: I don't know your name yet! Please tell me."; // Prompt for the name
        }

        // Check if the user expresses interest in a topic
        if (input.Contains("interested in"))
        {
            int index = input.IndexOf("interested in") + "interested in".Length; // Find the index of the topic
            string topic = input.Substring(index).Trim(); // Extract the topic
            favoriteTopic = topic; // Store the favorite topic
            currentTopic = topic; // Set the current topic
            return $"ChatBot: Great! I'll remember that you're interested in {topic}. It's an important part of staying safe online.";
        }

        // Check if the user wants more information on the current topic
        if ((input.Contains("tell me more") || input.Contains("more info")) && !string.IsNullOrEmpty(currentTopic))
        {
            if (keywordResponses.TryGetValue(currentTopic, out var tips)) // Retrieve tips for the current topic
                return "ChatBot: " + tips[random.Next(tips.Count)]; // Return a random tip
            else
                return "ChatBot: Can you clarify what topic you'd like more info on?"; // Prompt for clarification
        }

        // Detect sentiment from keywords
        foreach (var mood in sentimentKeywords)
        {
            foreach (var word in mood.Value)
            {
                if (input.Contains(word)) // Check if the input contains any sentiment keywords
                {
                    var responses = sentimentDynamicResponses[mood.Key]; // Get responses for the detected sentiment
                    return "ChatBot: " + responses[random.Next(responses.Count)]; // Return a random sentiment response
                }
            }
        }

        // Check for exact matches in basic responses
        if (basicResponses.TryGetValue(input, out string exactResponse))
            return exactResponse; // Return the exact response if found

        // Find topics that match the user input
        var matchedTopics = keywordResponses.Keys
            .Where(keyword => input.Contains(keyword)) // Filter keywords that are present in the input
            .ToList();

        // If any topics are matched, generate a response
        if (matchedTopics.Count > 0)
        {
            currentTopic = matchedTopics.Last(); // Set the current topic to the last matched topic
            var responses = matchedTopics.Select(topic =>
            {
                var tip = keywordResponses[topic][random.Next(keywordResponses[topic].Count)]; // Get a random tip for each matched topic
                return $"{char.ToUpper(topic[0]) + topic.Substring(1)}: {tip}"; // Format the response
            });

            return "ChatBot: " + string.Join(" ", responses); // Return the combined response
        }

        // Default response if no matches are found
        return "ChatBot: I'm not sure I understand. Can you try rephrasing or ask something related to cybersecurity?";
    }

    // Method to display the welcome banner
    private void DisplayBanner()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray; // Set color for the banner
        Console.WriteLine("----------------------------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.White; // Set color for the text
        Console.WriteLine("*            Welcome to CyberSecurity Awareness ChatBot              *");
        Console.ForegroundColor = ConsoleColor.DarkGray; // Set color for the banner
        Console.WriteLine("----------------------------------------------------------------------");
        Console.ResetColor(); // Reset color to default
    }

    // Method to request the user's name
    private void RequestUserName()
    {
        Console.Write("ChatBot: ");
        effectTyping("Please enter your name.", ConsoleColor.Gray); // Prompt for the user's name

        do
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // Set color for user input
            Console.Write("You: ");
            Console.ForegroundColor = ConsoleColor.White; // Set color for the text
            userName = Console.ReadLine()?.Trim(); // Read and trim the user's input

            // Check if the user input is empty
            if (string.IsNullOrWhiteSpace(userName))
            {
                effectTyping("\nChatBot: Name cannot be empty. Please enter a valid name.", ConsoleColor.Red); // Prompt for a valid name
                Console.ResetColor(); // Reset color to default
            }
        } while (string.IsNullOrWhiteSpace(userName)); // Repeat until a valid name is provided
    }

    // Method to simulate typing effect for messages
    private void effectTyping(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color; // Set color for the message
        foreach (char c in message) // Iterate through each character in the message
        {
            Console.Write(c); // Print the character
            Thread.Sleep(25); // Pause for a brief moment to simulate typing
        }
        Console.WriteLine(); // Move to the next line
        Console.ResetColor(); // Reset color to default
    }
}
