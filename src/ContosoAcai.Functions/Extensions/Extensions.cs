using Microsoft.Extensions.Hosting;

namespace ContosoAcai.Functions.Extensions;

public static class Extensions
{
    public static IHostBuilder RegisterServices(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
        });
        
        return builder;
    }
}