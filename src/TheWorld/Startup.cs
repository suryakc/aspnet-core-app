using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using TheWorld.ViewModels;

namespace TheWorld
    {
    public class Startup
        {
        private IHostingEnvironment m_env;
        private IConfigurationRoot m_config;

        public Startup (IHostingEnvironment env)
            {
            m_env = env;

            var builder = new ConfigurationBuilder ()
                .SetBasePath(m_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            m_config = builder.Build ();
            }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services)
            {
            services.AddSingleton (m_config);

            if (!m_env.IsProduction ())
                services.AddScoped<IMailService, DebugMailService> (); //services.AddSingleton<IMailService, DebugMailService> ();
            else
                {
                // Implement a real mail service...
                }

            services.AddDbContext<WorldContext> ();

            services.AddScoped<IWorldRepository, WorldRepository> ();

            services.AddTransient<WorldContextSeedData> ();

            services.AddLogging ();

            services.AddMvc ()
                .AddJsonOptions (config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                });
            }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, WorldContextSeedData seeder, ILoggerFactory loggerFactory)
            {
            #region default code
            //loggerFactory.AddConsole();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            #endregion default code

            Mapper.Initialize (config =>
            {
                config.CreateMap<TripViewModel, Trip> ().ReverseMap ();
                config.CreateMap<StopViewModel, Stop> ().ReverseMap ();
            });

            if (!env.IsProduction ())
                {
                app.UseDeveloperExceptionPage ();
                loggerFactory.AddDebug (LogLevel.Information);
                }
            else
                {
                loggerFactory.AddDebug (LogLevel.Error);
                }

            //app.UseDefaultFiles();

            app.UseStaticFiles ();

            app.UseMvc (config =>
             {
                 config.MapRoute (
                     name: "Default",
                     template: "{controller}/{action}/{id?}",
                     defaults: new
                         {
                         controller = "App",
                         action = "Index"
                         }
                     );
             });

            seeder.EnsureSeedData ().Wait ();
            }
        }
    }
