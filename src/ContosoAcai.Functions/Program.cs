using ContosoAcai.Functions.Extensions;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .RegisterServices()
    .Build();

host.Run();