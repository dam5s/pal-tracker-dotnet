﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PalTracker
{
    public static class ConfigurationExtensions
    {
        public static string Env(this IConfiguration config, string name) =>
            config.GetValue(name, $"{name} not configured");
    }
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton(sp => new WelcomeMessage(
                Configuration.Env("WELCOME_MESSAGE")
            ));

            services.AddSingleton(sp => new CloudFoundryInfo(
                Configuration.Env("PORT"),
                Configuration.Env("MEMORY_LIMIT"),
                Configuration.Env("CF_INSTANCE_INDEX"),
                Configuration.Env("CF_INSTANCE_ADDR")
            ));

            services.AddSingleton<ITimeEntryRepository, InMemoryTimeEntryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
