using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Standard.TransactionSimulator.Applicator.Configurations;
using System;
using System.IO;

namespace Standard.TransactionSimulator.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IConfigurationBuilder Builder { get; }

        public Startup(IWebHostEnvironment env)
        {
            Builder = new ConfigurationBuilder().SetBasePath(Path.Combine(env.ContentRootPath, "Settings"))
                                                .AddJsonFile($"connectionstrings.json", true, true)
                                                .AddJsonFile($"appsettings.json", true, true)
                                                .AddJsonFile($"messagebroker.{env.EnvironmentName}.json", true, true)
                                                .AddEnvironmentVariables();

            Configuration = Builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutofac();
            services.AddMemoryCache();
            services.ConfigureOptions(Configuration);

            ContainerBuilder container = new ContainerBuilder();

            container.Populate(services);
            container.RegisterModule(new MediatorModuleConfiguration());
            container.RegisterModule(new ApplicationModuleConfiguration(Configuration));

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.ConfigureEventBus();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            //app.StartSimulator();
        }
    }
}
