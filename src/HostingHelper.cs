using Serilog;
using Microsoft.Extensions.Configuration;

namespace Microsoft.AspNetCore.Hosting
{
    public static class HostingHelper
    {
        public static IWebHostBuilder ConfigureWebHostBuilder(this IWebHostBuilder builder, System.Action<WebHostBuilderContext, HostingHelperConfiguration> cfg = null)
        {

            HostingHelperConfiguration hostingConfig = new HostingHelperConfiguration();


            //detecting run in container
            bool IsInDocker = System.Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
            // set listen port
            int.TryParse(System.Environment.GetEnvironmentVariable("VIRTUAL_PORT") ?? "58000", out int port);

            builder
                .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                if (cfg != null)
                {
                    cfg(hostingContext, hostingConfig);
                }

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                hostingConfig?.ConfigFiles.ForEach(cf => config.AddJsonFile(cf.Path, cf.Optional));

                config.AddEnvironmentVariables();
            })
            .UseSerilog((hostingContext, loggerConfiguration) =>
            {
                Log.CloseAndFlush();
                loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
                //.WriteTo.Console()
                ;
            });

            if (IsInDocker)
            {
                builder.UseUrls($"http://0.0.0.0:{port}");
            }
            else
            {
                builder.UseUrls($"http://127.0.0.1:{port}");
            }
            return builder;
        }
    }
}