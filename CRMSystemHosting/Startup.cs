using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WellsFargo.Libraries.CRMSystem.Domain.Impl;
using WellsFargo.Libraries.CRMSystem.Domain.Interfaces;
using WellsFargo.Libraries.CRMSystem.ORM.Impl;
using WellsFargo.Libraries.CRMSystem.ORM.Interfaces;
using WellsFargo.Libraries.CRMSystem.Validations.Impl;
using WellsFargo.Libraries.CRMSystem.Validations.Interfaces;
using WellsFargo.Libraries.DataAccess.Impl;
using WellsFargo.Libraries.DataAccess.Interfaces;

namespace CRMSystemHosting
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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
