using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Mono.Unix.Native;

namespace FDK
{
    class Server{
        public static IHostBuilder CreateHostBuilder(IContainerEnvironment ctnEnv)
        {   
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                              .UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {

                        if (!ctnEnv.FN_LISTENER.Contains("unix:")) {
                          //...
                          return;
                        }

                        var socketPath = ctnEnv.FN_LISTENER.Replace("unix:", "");
                        var socketDir = Path.GetDirectoryName(socketPath);
                        var socketFile = Path.GetFileName(socketPath);
                        var symlinkFile = $"phony-{socketFile}";
                        
                        var symlinkSocketPath = Path.Join(socketDir, symlinkFile);

                        Console.WriteLine("Base Dir: {0}, File: {1}", socketDir, socketFile);

                        //Cleaning up the sock file and the soft link to sock file, if it does not get deleted
                        if(File.Exists(socketPath))
                        {
                            File.Delete(socketPath);
                        }
                        if(File.Exists(symlinkSocketPath))
                        {
                            File.Delete(symlinkSocketPath);
                        }
                        Console.WriteLine("FN_LISTENER: {0}", ctnEnv.FN_LISTENER);
<<<<<<< HEAD
                        options.ListenUnixSocket(ctnEnv.FN_LISTENER);

                        string sre = "Hello";
                        sre.Contains("T");
=======
                        options.ListenUnixSocket(symlinkSocketPath);
>>>>>>> 4773ff5cebaa9e08fee4bad60aae195055075499
                        Console.WriteLine("Unix Domain Socket connected");

                        Syscall.chmod(
                            symlinkSocketPath,
                            NativeConvert.FromOctalPermissionString ("0666")
                        );

                        Syscall.symlink(symlinkFile, socketPath);
                    });
                });
        }
    }
}
