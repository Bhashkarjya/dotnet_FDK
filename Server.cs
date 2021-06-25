using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace FDK
{
    class Server{
        public static IHostBuilder CreateHostBuilder(IContainerEnvironment ctnEnv)
        {
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {
                        //options.ListenUnixSocket(ctnEnv.FN_LISTENER);
                        Console.WriteLine("Unix Domain Socket connected");
                    });
                });
        }
    }
}