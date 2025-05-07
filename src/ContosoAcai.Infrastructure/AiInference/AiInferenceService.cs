using Azure;
using Azure.AI.OpenAI;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.AiInference;

public partial class AiInferenceService
{
    private AzureOpenAIClient CreateClient(ApiKeyCredentials credentials)
    {
        return new AzureOpenAIClient(new Uri(
                credentials.Endpoint), 
            new AzureKeyCredential(credentials.Key));
    }
}