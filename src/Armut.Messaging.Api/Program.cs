using Armut.Messaging.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Armut.Messaging.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseLogging();
    }
}
