using Azure;
using Azure.AI.DocumentIntelligence;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.Speech;

public partial class SpeechService
{
    private DocumentIntelligenceClient CreateClient(ApiKeyCredentials credentials)
    {
         return new DocumentIntelligenceClient(new Uri(
                credentials.Endpoint), 
            new AzureKeyCredential(credentials.Key));
    }
}