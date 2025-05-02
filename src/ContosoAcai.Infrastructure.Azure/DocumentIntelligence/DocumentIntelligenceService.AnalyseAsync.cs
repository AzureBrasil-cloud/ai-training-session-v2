using Azure;
using Azure.AI.DocumentIntelligence;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.DocumentIntelligence;

public partial class DocumentIntelligenceService
{
    public async Task<List<string>> AnalyseAsync(
        ApiKeyCredentials credentials,
        Stream document)
    {
        var binaryData = await BinaryData.FromStreamAsync(document);
        var client = CreateClient(credentials);
        
        var operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, 
            new AnalyzeDocumentOptions("prebuilt-layout", binaryData)
            {
                Features = { new DocumentAnalysisFeature("keyValuePairs") },
            });

        var values = operation.Value.KeyValuePairs
            .Where(kvp => kvp.Key != null)
            .Select(kvp => $"{kvp.Key.Content}: {kvp.Value?.Content ?? string.Empty}")
            .ToList();
        
        return values;
    }
}