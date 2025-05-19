using System.Text.Json;
using System.Text.Json.Schema;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using ContosoAcai.Infrastructure.Azure.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using OpenAI.Chat;

namespace ContosoAcai.Infrastructure.AiInference;

public partial class AiInferenceService
{
    public virtual async Task<string> CompleteAsync(
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
                
        var result = await client.CompleteChatAsync(chat);

        return result.Value.Content[0].Text;
    }

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
        
        var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            WriteIndented = true,
            NumberHandling = JsonNumberHandling.Strict,
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            RespectNullableAnnotations = true,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };
        
        var exporterOptions = new JsonSchemaExporterOptions
        {
            TreatNullObliviousAsNonNullable = true
        };
        
        JsonSerializerOptions options = JsonSerializerOptions.Default;
        var jsonSchema = options.GetJsonSchemaAsNode(typeof(T), exporterOptions).ToJsonString(serializerOptions);
        
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