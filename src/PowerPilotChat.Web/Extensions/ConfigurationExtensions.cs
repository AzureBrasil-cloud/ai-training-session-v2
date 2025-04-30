using PowerPilotChat.Application.Extensions;
using PowerPilotChat.Web.Middlewares;

namespace PowerPilotChat.Web.Extensions;

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

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseDefaultFiles();
        app.MapStaticAssets();
        
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}")
            .WithStaticAssets();
        
        app.MapFallbackToFile(app.Environment.IsDevelopment() ? "/index_dev.html" : "/client/index.html");
    }
}