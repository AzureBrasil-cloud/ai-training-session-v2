using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ContosoAcai.Functions.Functions;

public class EmailSenderFunction(ILogger<EmailSenderFunction> logger)
{
    [Function(nameof(EmailSenderFunction))]
    public async Task Run([QueueTrigger("input", Connection = "AzureQueue:ConnectionsString")] QueueMessage queueMessage)
    {
        var messageBody = queueMessage.Body.ToString(); // OR:
        logger.LogWarning(messageBody);
    }
}