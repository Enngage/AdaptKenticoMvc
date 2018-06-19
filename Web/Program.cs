using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:51355", "https://richardsadaptmvc.azurewebsites.net")
                .Build();
    }
}
