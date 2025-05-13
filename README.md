CyberSecurity Awareness ChatBot

A console-based C# chatbot designed to educate users on essential cybersecurity topics in a conversational and interactive way. The chatbot uses keyword recognition, sentiment detection, and dynamic response generation to assist users in understanding online safety practices.

Features

Keyword Recognition: Detects cybersecurity-related keywords (e.g., "phishing", "password") and provides relevant tips.

Sentiment Detection: Responds to emotional cues such as "worried" or "confused" to offer empathy and support.

Memory & Context Awareness:

Remembers user's name.

Tracks user's topic of interest.

Supports follow-up questions like "Tell me more".

Basic Responses: Responds to general greetings and FAQs.

Colorful Console Output: Uses color and typing effects for a more engaging terminal experience.

Getting Started

Prerequisites

.NET SDK installed (.NET 6 or later recommended)

A C# compatible IDE (e.g., Visual Studio, Visual Studio Code)

How to Run

Clone or download the repository.

Open the project in your IDE.

Build the solution.

Run the project.

Alternatively, using terminal:

dotnet run

Code Structure

ChatBot class contains:

StartChat(): Manages the conversation flow.

InitializeResponses(): Initializes all keyword, sentiment, and basic responses.

GetResponse(): Main logic for parsing and replying to user input.

effectTyping(): Simulates a chatbot typing effect for better UX.

RequestUserName(), DisplayBanner(): Utility methods to welcome and interact with the user.

Example Topics Covered

Password Safety

Phishing Awareness

Online Scams

Privacy Tips

Malware Protection

Network Security

Future Enhancements

Add file logging for conversations.

Enable saving/restoring chat sessions.

Integrate Natural Language Processing (NLP) for better understanding.

Build a GUI interface using WPF or Windows Forms.

