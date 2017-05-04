using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Services;
using Models.Services;
using Data;
using CsvHelper.Configuration;
using WebApiContrib.Core.Formatter.Csv;

namespace Stat
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            // Add framework services.
            services.AddMvc()
                    .AddCsvSerializerFormatters(new CsvFormatterOptions {
                        CsvDelimiter = ","
                    });


            //Register services
            var csvConfig = new CsvConfiguration() { Delimiter = Configuration.GetValue<string>("CsvDelimeter") };
            csvConfig.RegisterClassMap<DataStagingMap>();

            services.AddScoped<ICsvService>(c => new CsvService(
                Configuration.GetConnectionString("DefaultConnection"),
                Configuration.GetValue<string>("CsvStagingTable"),
                Configuration.GetSection("CsvStagingTableCols").Get<string[]>(),
                csvConfig,
                services.BuildServiceProvider().GetService<StatContext>()));

            services.AddScoped<IRankingsService, RankingService>();

            services.AddDbContext<StatContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseDeveloperExceptionPage();
            // app.UseStatusCodePages();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
