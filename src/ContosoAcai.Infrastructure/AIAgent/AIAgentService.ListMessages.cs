using System.Text;
using Azure;
using Azure.AI.Agents.Persistent;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;
using MessageTextContent = Azure.AI.Agents.Persistent.MessageTextContent;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<IEnumerable<Message>> ListMessageAsync(Credentials credentials, string threadId)
    {
        var client = CreateAgentsClient(credentials);
        AsyncPageable<PersistentThreadMessage> messages = client.Messages.GetMessagesAsync(threadId, order: ListSortOrder.Ascending);

        var result = new List<Message>();

        await foreach (var threadMessage in messages)
        {
            var content = threadMessage.ContentItems
                .OfType<MessageTextContent>()
                .Aggregate(new StringBuilder(), (builder, item) => builder.AppendLine(item.Text))
                .ToString();

            result.Add(new Message(threadMessage.Id, MessageRole.User.ToString(), content));
        }

        return result;
    }
}