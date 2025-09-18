using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // для OpenApiInfo (опционально)s
using RepairShopAPI.Models;

namespace RepairShopAPI
{
    internal class Program
    {        
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Logger logger = Logger.Instance;
            
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<RepairShopContext>(opts =>
                opts.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
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
                    Description = "Введите: Bearer {token}"
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