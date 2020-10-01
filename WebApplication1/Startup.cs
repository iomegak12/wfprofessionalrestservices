using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using WellsFargo.Libraries.CRMSystem.Domain.Impl;
using WellsFargo.Libraries.CRMSystem.Domain.Interfaces;
using WellsFargo.Libraries.CRMSystem.ORM.Impl;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.CRMSystem.Validations.Impl;
using WellsFargo.Libraries.CRMSystem.Validations.Interfaces;
using WellsFargo.Libraries.DataAccess.Impl;
using WellsFargo.Libraries.DataAccess.Interfaces;
using WellsFargo.Libraries.Hosting.Extensibility;

namespace WebApplication1
{
    public class Startup
    {
        private const string INVALID_CONNECTION_STRING = "Invalid CRM System DB Connection String Specified!";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("CRMSystemDBConnectionString");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ApplicationException(INVALID_CONNECTION_STRING);
            }

            services.AddDbContext<CustomerProfilesContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });

            services.AddScoped<ICustomerProfilesContext, CustomerProfilesContext>();
            services.AddScoped<ICustomerProfilesRepository, CustomerProfilesRepository>();
            services.AddScoped<ICustomerProfileDomainService, CustomerProfileDomainService>();
            services.AddScoped<IDomainValidation<string>, SearchStringValidation>();
            services.AddScoped<IProductReviewsContext, ProductReviewsContext>();
            services.AddScoped<IProductReviewDatabaseSettings, ProductReviewDatabaseSettingsImpl>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(bearerOptions =>
                    {
                        bearerOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = this.Configuration["Jwt:Issuer"],
                            ValidAudience = this.Configuration["Jwt:Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"])),
                            ValidAudiences = new List<string>
                            {
                                this.Configuration["Jwt:Issuer"]
                            }
                        };
                    });

            services.AddSwaggerGen(swaggerGenOption =>
            {
                swaggerGenOption.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Customers API",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "service-ref@wellsfargo.com",
                        Name = "WellsFargo Engineering Team",
                        Url = new Uri(@"https://engineering.wellsfargo.com/crmsystem")
                    },
                    Description = "Simple Customers API",
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Apache 2.0",
                        Url = new Uri(@"https://services.wellsfargo.com/license")
                    },
                    TermsOfService = new Uri(@"https://services.wellsfargo.com/terms")
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, @"WellsFargo.Libraries.CRMSystem.Services.Impl.xml");

                swaggerGenOption.IncludeXmlComments(filePath);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers API v1");
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
