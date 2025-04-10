using Heimdallr.Security.Scrypt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.SpaServices;
using VueCliMiddleware;

namespace Heimdallr.Host;

public class Startup
{
    private readonly IHostEnvironment _env;

    public Startup(IConfiguration configuration, IHostEnvironment env)
    {
        _env = env;
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddScryptModule();
        services
            .AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Heimdallr API",
                    Description = "Password Generator API"
                });
                c.CustomSchemaIds(type => type.FullName);
                c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_env.ApplicationName}.xml");

            });
        // In production, the Vue files will be served from this directory
        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist";
        });
    }

    /// <summary>
    /// Configure pipeline
    /// </summary>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heimdallr API");
        });

        app.UseSpaStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            if (env.IsDevelopment())
            {
                endpoints.MapToVueCliProxy(
                    "{*path}",
                    new SpaOptions { SourcePath = "ClientApp" },
                    npmScript: "serve",
                    regex: "Compiled successfully",
                    forceKill: true
                );
            }
        });

        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = "ClientApp";
        });
    }
}
