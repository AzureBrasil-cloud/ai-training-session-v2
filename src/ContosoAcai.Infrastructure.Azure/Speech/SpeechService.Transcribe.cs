using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.Speech;

public partial class SpeechService(IHttpClientFactory httpClientFactory)

{
    public async Task<SpeechTranscription> TranscribeAsync(
        ApiKeyCredentials credentials, 
        Stream audioStream,
        string fileName)
    {
        var httpClient = httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(credentials.Endpoint);
        // Add headers
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.Key);
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        using var formData = new MultipartFormDataContent();
        formData.Headers.ContentType!.MediaType = "multipart/form-data";

        // Add audio file to form-data
        formData.Add(new StreamContent(audioStream), "audio", fileName);

        // Add definition JSON to form-data
        var definition = new
        {
            locales = new[] { "pt-BR" },
            profanityFilterMode = "Masked",
            channels = new[] { 0 }
        };

        var definitionContent = new StringContent(JsonSerializer.Serialize(definition), Encoding.UTF8);
        formData.Add(definitionContent, "definition");

        // Make the HTTP POST request
        var response =
            await httpClient.PostAsync("/speechtotext/transcriptions:transcribe?api-version=2024-05-15-preview",
                formData);

        // Ensure the response is successful
        response.EnsureSuccessStatusCode();

        // Read and return the response content
        var result = await response.Content.ReadFromJsonAsync<SpeechTranscription>();
        return result!;
    }
}