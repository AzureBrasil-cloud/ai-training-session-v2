using System.Diagnostics;
using Azure.Monitor.OpenTelemetry.Exporter;
using ContosoAcai.Application;
using ContosoAcai.Application.Extensions;
using PowerPilotChat.Web.Middlewares;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;

namespace ContosoAcai.Web.Extensions;

public static class ConfigurationExtensions
{
    public static void AddConfigurations(
        this IServiceCollection services, 
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var mvcBuilder = services.AddControllersWithViews();
        
        if (environment.IsDevelopment())
        {
            mvcBuilder.AddRazorRuntimeCompilation();
        }
        
        services.AddHttpContextAccessor();

        // Application
        services.AddApplication(configuration);
        
        // Global exception handler
        services.AddTransient<GlobalExceptionHandlerMiddleware>();
        
        AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true); 
        AppContext.SetSwitch("Azure.Experimental.TraceGenAIMessageContent", true);

        services.AddOpenTelemetry()
            .WithTracing(tracing => tracing
                .ConfigureResource(resource => resource.AddService("ConstosoAcai.Api"))    
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSource(InstrumentationConfig.ActivitySource.Name)
                .AddOtlpExporter()
                .AddAzureMonitorTraceExporter(options =>
                {
                    options.ConnectionString = configuration["AzureMonitor:ConnectionString"]!;
                }))
            .WithMetrics(metrics => metrics
                .ConfigureResource(resource => resource.AddService("ConstosoAcai.Api"))    
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter()
                .AddAzureMonitorMetricExporter(options =>
                {
                    options.ConnectionString = configuration["AzureMonitor:ConnectionString"]!;
                }));
    }

    public static void ConfigureApplication(this WebApplication app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseDefaultFiles();
        app.MapStaticAssets();
        
        app.MapControllers();
        
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}")
            .WithStaticAssets();
        
        app.MapFallbackToFile(app.Environment.IsDevelopment() ? "/index_dev.html" : "/client/index.html");
    }
}