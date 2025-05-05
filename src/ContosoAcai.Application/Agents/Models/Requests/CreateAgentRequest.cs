namespace ContosoAcai.Application.Agents.Models.Requests;

public record CreateAgentRequest(AgentType Type);

public enum AgentType
{
    Guide = 1,
    Sales = 2
}
