using ContosoAcai.Application.Threads.Models.Results;
using ContosoAcai.Infrastructure.Azure.Shared;
using PowerPilotChat.Application;

namespace ContosoAcai.Application.Threads;

public partial class ThreadService
{
    public async Task<Result<ThreadResult>> CreateAsync()
    {
        var thread = await aiAgentService.CreateThreadAsync(CreateCredentials());
        return Result<ThreadResult>.Success(new ThreadResult(thread.Id));
    }
    
    private Credentials CreateCredentials()
    {
        return new Credentials(
            configuration["AI:Agent:TenantId"]!,
            configuration["AI:Agent:ClientId"]!,
            configuration["AI:Agent:ClientSecret"]!,
            configuration["AI:Agent:ConnectionString"]!);
    }
}