using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseKestrel()
                .ConfigureKestrel(options => { options.ListenAnyIP(8080); })
                .UseIIS()
                .UseUrls(
                    "http://localhost:51355",
                    "https://richardsadaptmvc.azurewebsites.net",
                    "https://kentico-adapt-live.azurewebsites.net");
    }
}
