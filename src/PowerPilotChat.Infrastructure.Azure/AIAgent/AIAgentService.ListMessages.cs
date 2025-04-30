using System.Text;
using Azure.AI.Projects;
using PowerPilotChat.Infrastructure.Azure.AIAgent.Models;
using PowerPilotChat.Infrastructure.Azure.Shared;

namespace PowerPilotChat.Infrastructure.Azure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<IEnumerable<Message>> ListMessageAsync(
        Credentials credentials,
        string connectionString,
        string threadId)
    {
        var client = CreateAgentsClient(credentials, connectionString);
        var messages = (await client.GetMessagesAsync(threadId, order: ListSortOrder.Ascending)).Value.Data;

        return messages.Select(threadMessage =>
        {
            var content = threadMessage.ContentItems
                .OfType<MessageTextContent>()
                .Aggregate(new StringBuilder(), (builder, item) => builder.AppendLine(item.Text))
                .ToString();

            return new Message(content, threadMessage.Role == MessageRole.User);
        }).ToList();
    }
}