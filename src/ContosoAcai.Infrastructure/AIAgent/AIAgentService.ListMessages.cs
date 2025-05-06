using System.Text;
using Azure.AI.Projects;
using ContosoAcai.Infrastructure.AIAgent.Models;
using ContosoAcai.Infrastructure.Azure.Shared;

namespace ContosoAcai.Infrastructure.AIAgent;

public partial class AiAgentService
{
    public virtual async Task<IEnumerable<Message>> ListMessageAsync(Credentials credentials, string threadId)
    {
        var client = CreateAgentsClient(credentials);
        var messages = (await client.GetMessagesAsync(threadId, order: ListSortOrder.Ascending)).Value.Data;

        return messages.Select(threadMessage =>
        {
            var content = threadMessage.ContentItems
                .OfType<MessageTextContent>()
                .Aggregate(new StringBuilder(), (builder, item) => builder.AppendLine(item.Text))
                .ToString();

            return new Message(threadMessage.Id, MessageRole.User.ToString(), content);
        }).ToList();
    }
}