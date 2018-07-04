using Adapt;
using CloudIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        const string AppConfigPath = "App";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // map app settings
            services.Configure<AppConfig>(Configuration.GetSection(AppConfigPath));
            var config = Configuration.GetSection(AppConfigPath).Get<AppConfig>();

            services.AddCors();
            services.AddMvc();
            services.AddRouting();

            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<ICourseService, CourseService>(
                service => new CourseService(config.ProjectIds));
            services.AddScoped<IAdaptService, AdaptService>();
            services.AddScoped<IFileService, FileService>(
                service => new FileService(new FileServiceConfig()
                {
                    RootFolder = config.Files.RootFolder,
                    BlocksFilename = config.Files.BlocksFilename,
                    ComponentsFilename = config.Files.ComponentsFilename,
                    ArticlesFilename = config.Files.ArticlesFilename,
                    ContentObjectsFilename = config.Files.ContentObjectsFilename,
                    CoursesFolderName = config.Files.CoursesFolderName,
                    CourseFilename = config.Files.CourseFilename,
                    DefaultCourseJsonDataFilename = config.Files.DefaultCourseJsonDataFilename,
                    DefaultDataFolderName = config.Files.DefaultDataFolderName
                })
             );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            #warning Enable cors only for required domains when going live
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("api", "api/{controller}/{action}");
            });
        }
    }
}
