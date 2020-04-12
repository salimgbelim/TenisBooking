using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TennisBookings.Web.Configuration;
using TennisBookings.Web.Middleware;
using TennisBookings.Web.Services;

namespace TennisBookings.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWeatherForecaster, AmazingWeatherForecaster>();
           
            // below gives new GuidService for every request
            //services.AddTransient<GuidService>();
            
            // below gives singleton object for every request
            services.AddSingleton<GuidService>();
            
            // DI an static configuration class
            services.Configure<FeaturesConfiguration>(Configuration.GetSection("Features"));
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseMiddleware<CustomMiddleware>();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseMvc();
        }
    }
}
