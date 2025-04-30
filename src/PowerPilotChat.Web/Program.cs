using PowerPilotChat.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConfigurations(builder.Configuration, builder.Environment);

var app = builder.Build();

app.ConfigureApplication();

app.Run();
