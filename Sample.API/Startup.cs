using JWTSimpleServer.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.CloudFoundry;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;

namespace Sample.API
{
    public class Startup
    {
        public const string SigningKey = "Sample.api.SigningKey";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCloudFoundryActuators(Configuration);

            services.AddSingleton<IAuthenticationProvider, AuthenticationProvider>()
             .AddJwtSimpleServer(setup =>
             {
                 setup.IssuerSigningKey = SigningKey;
             })
             .AddJwtInMemoryRefreshTokenStore()
             .AddAuthorization();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "SAMPLE API",
                    Description = "SAMPLE API Description",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "A SAMPLE COMPANY", Email = "", Url = "https://sample.com" }
                });

                // Swagger 2.+ support:
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                // NOTE:
                // Problems authorizing from swagger?
                // You have to write 'Bearer {token}' in the Authorize input 
                // A lot of people is having similar issue ;)
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(security);

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "sample.api.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // Add management endpoints into pipeline
            app.UseCloudFoundryActuators();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VIN API V1");
            });

            app.UseJwtSimpleServer(setup =>
            {
                setup.IssuerSigningKey = SigningKey;
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
