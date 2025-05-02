using Azure;
using Azure.AI.Inference;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AiInference;

public partial class AiInferenceService
{
    private ChatCompletionsClient CreateClient(ApiKeyCredentials credentials)
    {
        return new ChatCompletionsClient(new Uri(
                credentials.Endpoint), 
            new AzureKeyCredential(credentials.Key));
    }
}