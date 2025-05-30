using System.Text;
using Azure;
using Azure.AI.Agents.Persistent;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;
using MessageRole = Azure.AI.Agents.Persistent.MessageRole;
using RunStatus = Azure.AI.Agents.Persistent.RunStatus;
using ThreadRun = Azure.AI.Agents.Persistent.ThreadRun;
using ToolOutput = Azure.AI.Agents.Persistent.ToolOutput;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    private const string AnnotationMark = "📖";
    
    public async Task<(Message Message, long InputTokenCount, long OutputTokenCount)> CreateRunAsync(
        Credentials credentials,
        string threadId,
        string userMessage,
        string agentId)
    {
        var client = CreateAgentsClient(credentials);

        await client.Messages.CreateMessageAsync(
            threadId,
            MessageRole.User,
            userMessage);
        
        Response<ThreadRun> runResponse = await client.Runs.CreateRunAsync(
            threadId,
            agentId,
            additionalInstructions: "");

        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            runResponse = await client.Runs.GetRunAsync(threadId, runResponse.Value.Id);
            
            if (runResponse.Value.Status == RunStatus.RequiresAction
                && runResponse.Value.RequiredAction is SubmitToolOutputsAction submitToolOutputsAction)
            {
                List<ToolOutput> toolOutputs = new();
                foreach (RequiredToolCall toolCall in submitToolOutputsAction.ToolCalls)
                {
                    toolOutputs.Add(await GetResolvedToolOutputAsync(toolCall));
                }
                runResponse = await client.Runs.SubmitToolOutputsToRunAsync(runResponse.Value, toolOutputs);
            }
            
        } while (runResponse.Value.Status == RunStatus.Queued || runResponse.Value.Status == RunStatus.InProgress);

        if (runResponse.Value.Status == RunStatus.Failed)
        {
            return (new Message(Guid.NewGuid().ToString(), MessageRole.User.ToString(), $"Error: {runResponse.Value.LastError.Message}"),
                0,
                0);
        }
        
        var afterRunMessagesResponse = client.Messages.GetMessagesAsync(
                threadId, 
                order: ListSortOrder.Descending, 
                limit: 1);
        
        var message = afterRunMessagesResponse.GetAsyncEnumerator().Current;
        
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

        return (new Message(message.Id, message.Role.ToString(), text.ToString()), 
            runResponse.Value.Usage.PromptTokens, 
            runResponse.Value.Usage.CompletionTokens);
    }
}