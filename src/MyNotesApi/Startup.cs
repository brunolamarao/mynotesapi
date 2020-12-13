using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using MyNotes.Data;

namespace MyNotesApi
{
    public class Startup
    {         
        protected readonly Container _container = new SimpleInjector.Container();

        protected WebAppSettings _settings;
        
        public Startup()
        {
            ConfigureSettings();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSimpleInjector(_container, options =>
            {
                options
                    .AddAspNetCore()
                    .AddControllerActivation()
                    .AddViewComponentActivation();
            });

            services.AddLogging();

            services.AddCors(options =>
            {
                options.AddPolicy("MyNotesApplication",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000");
                    });
            });
        }   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            RegisterComponents(app);

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterComponents(IApplicationBuilder app)
        {
            var neo4jAdapter = new Neo4jAdapter(_settings.Neo4jSettings);
            neo4jAdapter.Register(_container);
        }

        private void ConfigureSettings()
        {
            _settings = new WebAppSettings();

            var configuration = GetConfiguration();
            configuration.Bind(_settings);
        }

        private IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                          .AddJsonFile("appsettings.Development.json", optional:true, reloadOnChange:true)                          
                          .AddEnvironmentVariables();

            var config = builder.Build();

            return config;
        }
    }
}