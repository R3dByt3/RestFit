using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using RestFit.API.Extensions;
using RestFit.API.Middleware;
using RestFit.DataAccess.ClassMaps;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

namespace RestFitAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ClassMapCollection.Init();

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((hostContext, services, configuration) => {
                configuration
                .WriteTo.Console()
                .WriteTo.MongoDBBson("mongodb://localhost:27017/RestFit", "Log", Serilog.Events.LogEventLevel.Verbose, 50, TimeSpan.FromSeconds(5), 1024, 50000);
            });

            // Add services to the container.

            builder.Services.AddMvc();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RestFit API",
                    Description = "RestFit Tool",
                    /*TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }*/
                });
                options.ExampleFilters();
                options.SchemaFilter<SwaggerExcludeFilter>();
                options.OperationFilter<SwaggerQueryParameterAttributeFilter>();
                options.ParameterFilter<SwaggerQueryParameterAttributeFilter>();
                options.EnableAnnotations();
                options.CustomSchemaIds(type => type.FullName);
                options.IncludeClassXmlDocs();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            }).AddSwaggerGenNewtonsoftSupport();

            builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddMvc().AddNewtonsoftJson();

            // configure DI for application services
            builder.Services.AddStartup();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}