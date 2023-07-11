using EventService.Common.Constants;
using EventService.Controllers;
using EventService.Domain.Services;
using EventService.Provider.Repositories;
using System.Configuration;

namespace EventService
{
    public class Startup
    {
        private readonly AppConfiguration _appConfiguration;
        public Startup(AppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddSingleton(_appConfiguration);
            services.AddScoped<IEventCommandService, EventCommandService>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddHttpClient<EventRepository>();
            //services.AddStackExchangeRedisCache(options =>
            //{
            //    options.Configuration = "your_redis_connection_string";
            //});

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }
    }
}
