using System;
using System.Collections.Generic;
using Bingo.Services;
using Bingo.Web.Models;
using Bingo.Web.OutputFormatters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var registry = RegisterObjectToCSVConverters(); 
            services.AddMvc(options => {
                 options.OutputFormatters.Insert(0, new CSVOutputFormatter(registry)); 
                 //This seems the only way to get the csv formater to run with the defaul json formatter running as that captures all content types.
                 //options.OutputFormatters.Add(new CSVOutputFormatter()); 
            });

            // Add application services.
            services.AddTransient<ISearchEngine, BingSearchService>();
        }

        private IDictionary<Type, IConvertTypeToCSV> RegisterObjectToCSVConverters()
        {
            var registry = new Dictionary<Type, IConvertTypeToCSV>(); 
            registry.Add(new SearchOutcome().GetType(), new SearchOutcomeCVSFormatter()); 
            return registry;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
