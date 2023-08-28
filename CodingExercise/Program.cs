using System.Reflection;
using CodingExercise.Interfaces;
using CodingExercise.Logging;
using CodingExercise.Mapper;
using CodingExercise.Middleware;
using CodingExercise.Repositories;
using CodingExercise.Services;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CodingExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container
            builder.Services.AddTransient<IVehicleRepository, DummyVehicleRepository>();
            builder.Services.AddTransient<ITransponderService, TransponderService>();
            builder.Services.AddTransient<IVehicleService, VehicleService>();
            builder.Services.AddTransient<ITransponderRepositoryFactory, TransponderRepositoryFactory>();

            builder.Services.AddScoped<ITransponderRepository, ClassicTransponderRepository>();
            builder.Services.AddScoped<ITransponderRepository, ModernTransponderRepository>();

            builder.Services.AddControllers();

            builder.Services.AddAutoMapper(typeof(VehicleProfile));

            // Add services to the container.
            builder.Services.AddAuthorization();

            //Add logging
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(new ConfigurationBuilder()
                    .AddJsonFile("seri-log.config.json")
                    .Build())
                .WriteTo.CustomSink()
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "E470 API",
                    Version = "v1",
                    Description = "E470 Vehicle Code Challenge",
                    Contact = new OpenApiContact
                    {
                        Name = "John Findlay",
                        Email = "jwfindlay15@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/johnwfindlay/")
                    }
                });

                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory, xmlFileName));
            });

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "E470 API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}