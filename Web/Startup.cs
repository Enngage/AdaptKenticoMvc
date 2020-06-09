using System.Linq;
using Adapt;
using CloudIntegration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Models;
using Web.Services;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        const string AppConfigPath = "App";
        private const string CorsName = "AllowAll";

        public Startup(IWebHostEnvironment env)
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

            services.AddCors(options => options.AddPolicy(CorsName, m =>
                    m.WithOrigins(config.Cors.AllowedDomains.ToArray())
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                )
            );
            services.AddRouting();

            services.AddMvc(options => { options.EnableEndpointRouting = true; });

            services.AddResponseCompression();

            services.AddScoped<IComponentService, ComponentService>();
            services.AddScoped<ICourseService, CourseService>(
                service => new CourseService(new CourseServiceConfig(config.Depth, config.Projects.Select(m => new CourseServiceProject()
                {
                    ProjectId = m.ProjectId,
                    PreviewApiKey = m.PreviewApiKey
                }).ToList())
            ));
            services.AddScoped<IAdaptService, AdaptService>();
            services.AddScoped<ICourseGenerateService, CourseGenerateService>();
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
                    DefaultDataFolderName = config.Files.DefaultDataFolderName,
                    CourseLogFilename = config.Files.CourseLogFilename
                })
             );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var config = Configuration.GetSection(AppConfigPath).Get<AppConfig>();

            app.UseRouting();
            app.UseCors(builder => builder
                .WithOrigins(config.Cors.AllowedDomains.ToArray())
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            // add resource compression - before UseMVC
            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            /*
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute("api", "api/{controller}/{action}");
            });
            */
        }
    }
}
