using ContosoAcai.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration, builder.Environment);

builder.AddLogging();

var app = builder.Build();

app.ConfigureApplication();

app.Run();
