using Azure.AI.Inference;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AiInference;

public partial class AiInferenceService
{
    public virtual async Task Complete(ApiKeyCredentials credentials)
    {
        var client = CreateClient(credentials);

        var requestOptions = new ChatCompletionsOptions()
        {
            Messages = {
                new ChatRequestSystemMessage("You are a helpful assistant."),
                new ChatRequestUserMessage("Explain Riemann's conjecture in 1 paragraph")
            },
            Model = "mistral-large"
        };

        var response = await client.CompleteAsync(requestOptions);
        Console.WriteLine($"Response: {response.Value.Content}");
    }
}