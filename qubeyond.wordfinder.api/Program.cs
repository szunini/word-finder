
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using qubeyond.wordfinder.domain.Contracts.Cache;
using qubeyond.wordfinder.domain.Contracts.Services;
using qubeyond.wordfinder.domain.Contracts.Validators;
using qubeyond.wordfinder.domain.Services;
using quebeyond.wordfinder.infraestructure.Cache;
using quebeyond.wordfinder.infraestructure.Validation;
using StackExchange.Redis;
using System.Reflection;

namespace qubeyond.wordfinder.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var redisConfiguration = ConfigurationOptions.Parse("rediscache:6379");
            var redis = ConnectionMultiplexer.Connect(redisConfiguration);

            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
            builder.Services.AddScoped<ICacheProvider, RedisCacheProvider>();
            builder.Services.AddScoped<IWordFinderService, WordFinderService>();
            builder.Services.AddScoped<IWordFinderValidator, WordFinderValidator>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qu Beyond Word Finder Challenge API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            var app = builder.Build();
            /*
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {*/
                app.UseSwagger();
                app.UseSwaggerUI();
            /*}
            */
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;
                    context.Response.StatusCode = 500;
                    if (exception != null)
                    {
                        await context.Response.WriteAsJsonAsync(new ErrorMessage(exception));
                    }
                    
                });
            });

            app.MapControllers();

            app.Run();
        }
    }
}
