using EventService;
using EventService.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var environment = builder.Environment;
        //var startup = new Startup();
        var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .Build();

        var appConfiguration = new AppConfiguration();
        configuration.GetSection("AppConfiguration").Bind(appConfiguration);

        // Pass the appConfiguration to ConfigureServices method
        var startup = new Startup(appConfiguration);

        // Configure services using Startup class
        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}