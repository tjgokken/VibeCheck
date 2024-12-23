﻿// Azure Cognitive Services credentials
using Azure.AI.TextAnalytics;
using Azure;

var endpoint = Environment.GetEnvironmentVariable("AzureCognitiveEndpoint");
var apiKey = Environment.GetEnvironmentVariable("AzureCognitiveServiceKey");

if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
{
    Console.WriteLine("Environment variables 'AzureCognitiveEndpoint' and 'AzureCognitiveServiceKey' must be set.");
    return;
}

// Create Text Analytics client
var client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));

Console.WriteLine("Enter feedback text:");
var feedback = Console.ReadLine();

if (!string.IsNullOrEmpty(feedback))
{
    var sentiment = AnalyzeSentiment(client, feedback);
    Console.WriteLine($"Sentiment: {sentiment.Sentiment}");
    Console.WriteLine($"Positive Score: {sentiment.ConfidenceScores.Positive}");
    Console.WriteLine($"Neutral Score: {sentiment.ConfidenceScores.Neutral}");
    Console.WriteLine($"Negative Score: {sentiment.ConfidenceScores.Negative}");

    if (sentiment.ConfidenceScores.Positive > 0.75)
    {
        Console.WriteLine("Thank you for your positive feedback!");
        Console.WriteLine($"Feedback: {feedback}");
    }
    else if (sentiment.ConfidenceScores.Negative > 0.75)
    {
        Console.WriteLine("ALERT: Negative feedback detected. Notifying support team...");
        NotifySupportTeam(feedback);
    }
    else
    {
        Console.WriteLine("Feedback is neutral or mixed.");
        Console.WriteLine($"Feedback: {feedback}");
    }

    static void NotifySupportTeam(string feedback)
    {
        Console.WriteLine($"Support team notified with the following feedback: {feedback}");
    }
}
else
{
    Console.WriteLine("Feedback cannot be empty!");
}

return;

static DocumentSentiment AnalyzeSentiment(TextAnalyticsClient client, string feedback)
{
    // Analyze sentiment using Azure Cognitive Services
    var response = client.AnalyzeSentiment(feedback);
    return response.Value;
}