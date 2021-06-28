using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

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
                        //Console.WriteLine(ctnEnv.FN_LISTENER);
                        //Console.WriteLine(ctnEnv.SYMBOLIC_LINK);
                        //Cleaning up the sock file and the soft link to sock file, if it does not get deleted
                        if(File.Exists(ctnEnv.FN_LISTENER))
                        {
                            File.Delete(ctnEnv.FN_LISTENER);
                        }
                        if(File.Exists(ctnEnv.SYMBOLIC_LINK))
                        {
                            File.Delete(ctnEnv.SYMBOLIC_LINK);
                        }
                        options.ListenUnixSocket(ctnEnv.FN_LISTENER);
                        Console.WriteLine("Unix Domain Socket connected");
                    });
                });
        }
    }
}