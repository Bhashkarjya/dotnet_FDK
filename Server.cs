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
                    webBuilder.
                    ConfigureKestrel(options =>
                    {
                        //string UnixListener = new ContainerEnvironment().FN_LISTENER;
                        //string ListenerUnixSocketPath = UnixListener.Substring(5);
                        options.ListenUnixSocket("/tmp/api.sock");
                        Console.WriteLine("Unix Domain Socket connected");
                        //Console.WriteLine(ListenerUnixSocketPath);
                    }).
                    UseStartup<Startup>();
                });
        }
    }
}