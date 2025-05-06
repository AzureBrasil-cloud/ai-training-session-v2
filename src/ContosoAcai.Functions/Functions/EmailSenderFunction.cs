using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Functions.Functions;

public class EmailSenderFunction(ILogger<EmailSenderFunction> logger)
{
    [Function(nameof(EmailSenderFunction))]
    [QueueOutput("output", Connection = "AzureQueue:ConnectionsString")]
    public string Run(
        [QueueTrigger("input", Connection = "AzureQueue:ConnectionsString")] QueueMessage queueMessage,
        FunctionContext context)
    {
        try
        {
            var messageBody = queueMessage.Body.ToString();
            var payload = JsonSerializer.Deserialize<EmailPayload>(messageBody);
            
            // Simulate sending the email (replace with actual logic if needed)
            logger.LogInformation("Sending email to {Email} with subject '{Subject}'", payload!.Email, payload.Subject);

            // Build response
            var result = new
            {
                Value = $"Email sent to {payload.Email} with subject '{payload.Subject}'",
                payload.CorrelationId
            };

            var outputMessage = JsonSerializer.Serialize(result);
            logger.LogInformation("Sent confirmation message to output queue: {Message}", outputMessage);

            return outputMessage;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to process queue message.");
            throw;
        }
    }

    private class EmailPayload
    {
        public string CorrelationId { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = null!;

        [JsonPropertyName("Body")]
        public string Body { get; set; } = null!;
    }
}
