using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace FDK
{
    class Server{
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {
                        options.ListenUnixSocket(new ContainerEnvironment().FN_LISTENER);
                        Console.WriteLine("Unix Domain Socket connected");
                    });
                });
        }
    }
}