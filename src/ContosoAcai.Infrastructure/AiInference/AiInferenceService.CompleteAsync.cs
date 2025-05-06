using ContosoAcai.Infrastructure.Azure.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using OpenAI.Chat;

namespace ContosoAcai.Infrastructure.Azure.AiInference;

public partial class AiInferenceService
{
    public virtual async Task<T> CompleteAsync<T>(
        ApiKeyCredentials credentials,
        string model,
        string instructions,
        string content)
    {
        var openAiClient = CreateClient(credentials);
      
        var client = openAiClient.GetChatClient(model);
        
        var chat = new List<ChatMessage>()
        {
            new SystemChatMessage(instructions),
            new UserChatMessage(content)
        };
        
        JSchemaGenerator generator = new JSchemaGenerator();
        var jsonSchema = generator.Generate(typeof(T)).ToString();
        
        var result = await client.CompleteChatAsync(
            chat,
            new ChatCompletionOptions()
            {
                ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                    "object",
                    BinaryData.FromString(jsonSchema))
            });

        return JsonConvert.DeserializeObject<T>(result.Value.Content[0].Text)!;
    }
}