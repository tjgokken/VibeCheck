# VibeCheck - Sentiment Analysis in .NET
From the accompanying blog article:

Sign in to Azure Portal, create an Azure Cognitive Services resource and note the API Key and endpoint URL. The project uses Environment Variables in Windows for those values.

After that we need to create a console application and add the following NuGet Package:

```bash
dotnet add package Azure.AI.TextAnalytics
```

And then add the following code in your Program.cs.

```csharp
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
```


You get multi-language support out of the box. 
