using Azure;
using Azure.AI.Inference;
using Azure.AI.OpenAI;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AiInference;

public partial class AiInferenceService
{
    private AzureOpenAIClient CreateClient(ApiKeyCredentials credentials)
    {
        return new AzureOpenAIClient(new Uri(
                credentials.Endpoint), 
            new AzureKeyCredential(credentials.Key));
    }
}