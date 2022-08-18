using _05_HireAndForget.Data;
using _05_HireAndForget.Repositories;
using _05_HireAndForget.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _05_HireAndForget {
    public class StartUp {
        public static WebApplication ItializeApplication(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            var app = builder.Build();
            Configure(app);
            return app;
        }

        private static void ConfigureServices(WebApplicationBuilder builder) {
            builder.Services.AddDbContext<AppDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ILogsRepository, LogsRepository>();
        }

        private static void Configure(WebApplication app) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}