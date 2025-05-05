using System.Text;
using Azure;
using Azure.AI.Projects;
using ContosoAcai.Infrastructure.Azure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    private const string AnnotationMark = "📖";
    
    public async Task<Message> CreateRunAsync(
        Credentials credentials,
        string threadId,
        string userMessage,
        string agentId)
    {
        var client = CreateAgentsClient(credentials);

        await client.CreateMessageAsync(
            threadId,
            MessageRole.User,
            userMessage);

        Response<ThreadRun> runResponse = await client.CreateRunAsync(
            threadId,
            agentId,
            additionalInstructions: "");

        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            runResponse = await client.GetRunAsync(threadId, runResponse.Value.Id);
        } while (runResponse.Value.Status == RunStatus.Queued || runResponse.Value.Status == RunStatus.InProgress);

        if (runResponse.Value.Status == RunStatus.Failed)
        {
            return new Message(
                Guid.NewGuid().ToString(), 
                MessageRole.User.ToString(),
                $"Error: {runResponse.Value.LastError.Message}");
        }
        
        Response<PageableList<ThreadMessage>> afterRunMessagesResponse =
            await client.GetMessagesAsync(
                threadId, 
                order: ListSortOrder.Descending, 
                limit: 1);

        var message = afterRunMessagesResponse.Value.Data.FirstOrDefault();

        if (message is null)
        {
            throw new Exception("No messages found after run.");
        }

        StringBuilder text = new();

        foreach (var contentItem in message.ContentItems)
        {
            if (contentItem is MessageTextContent textItem)
            {
                var annotations = textItem.Annotations;

                if (annotations.Any())
                {
                    var formattedText = textItem.Text;
                    
                    foreach (var annotation in annotations)
                    {
                        if (annotation is MessageTextFileCitationAnnotation messageTextFileCitationAnnotation)
                        {
                            formattedText = formattedText.Replace(messageTextFileCitationAnnotation.Text, $" ({AnnotationMark} {messageTextFileCitationAnnotation.FileId})");
                        }
                    }
                    text.AppendLine(formattedText);
                }
                else
                {
                    text.AppendLine(textItem.Text);
                }
            }
        }

        if (message.ContentItems.Count == 1 && text.Length > 0)
        {
            text.Length--;
        }

        return new Message(message.Id, message.Role.ToString(), text.ToString());
    }
}