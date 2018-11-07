# KSoft.AspNetCore.Hosting

1. Add package do Your project

```powershell
Install-Package KSoft.AspNetCore.Hosting
```

2. Set Up listening port with Environment varialbe: 
   
   ```cmd
   set LISTEN_ON = 59000
   ``` 

3. Use: 

```csharp
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Example.Site
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            builder
            .ConfigureWebHostBuilder((ctx, opts) =>
            {
                // HINT: you don't need to add appsettings.json this is by default
                // opts.ConfigFiles.Add(new JsonFile($"appsettings.json", optional: true));
                opts.ConfigFiles.Add(new JsonFile($"settings/logger.json", optional: true));
                opts.ConfigFiles.Add(new JsonFile($"settings/appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: true));
            })
            .UseStartup<Startup>();
            return builder;
        }
    }
}
```