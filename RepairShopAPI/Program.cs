using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;
using RepairShopAPI.Models;

//dotnet ef dbcontext scaffold Name=DefaultConnection Npgsql.EntityFrameworkCore.PostgreSQL -o Models -c RepairShopContext --force

namespace RepairShopAPI
{
    internal class Program
    {
        private readonly RepairShopContext _db = new();
        private static readonly string logPath = @"Log.txt";
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            File.AppendAllText(logPath, $"Logger started at [{DateTime.Now:dd.MM.yyyy HH:mm:ss}]");
            string s = Functions.GetHash("!!!!");
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();

            //builder.Services.AddDbContext<RepairShopContext>(opts =>
            //    opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            // controllers
            builder.Services.AddControllers();
            
            // --- SWAGGER ---
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RepairShop API", Version = "v1" });

                // ---- опция: если используешь JWT, можно добавить авторизацию в UI ----
                /*
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Введите: Bearer {token}"Waiting for pgAdmin 4 to start...
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
                */
            });

            var app = builder.Build();


            // Показывать swagger в Development (или всегда, если хочешь)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RepairShop API v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // если есть auth
            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}