using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace MSA.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Startup.Args = args;
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
