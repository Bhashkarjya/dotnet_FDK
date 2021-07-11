using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using Mono.Unix.Native;

namespace FDK
{
    public class Server{

        public static string _phonySock, _realSock;
        public static IHostBuilder CreateHostBuilder(IContainerEnvironment containerEnvironment)
        { 
            CheckFnFormat(containerEnvironment);
            return Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel()
                              .UseStartup<Startup>()
                    .ConfigureKestrel(options =>
                    {

                        string path = containerEnvironment.FN_LISTENER;
                        string path_scheme = path.Substring(0,containerEnvironment.SOCKET_TYPE.Length);
                        if(path_scheme != "unix")
                            Console.WriteLine("Url scheme must be unix with a valid path, got {0}", path_scheme);
                        
                        string realSock = path.Replace("unix:",""); 

                        if(realSock=="")
                        {
                            Console.WriteLine("Real socket file cannot be empty {0}", realSock);
                        }
                        string phonySock = Path.Join(Path.GetDirectoryName(realSock),Path.GetFileName(realSock)+".tmp");
                        Console.WriteLine("PhonySock: {0}\nRealSock: {1}",phonySock,realSock);
                        //DeleteFile(realSock);
                        //DeleteFile(phonySock);
                        _realSock=realSock;
                        _phonySock=phonySock;
                        options.ListenUnixSocket(phonySock);
                    });
                });
        }

        private static void CheckFnFormat(IContainerEnvironment containerEnvironment)
        {
            if(containerEnvironment.FN_FORMAT=="" || containerEnvironment.FN_FORMAT!="http-stream")
            {
                Console.WriteLine("only http-stream format is supported, please set function.format=http-stream against your fn service");
                System.Environment.Exit(0);
            }
        }

        public static void SockPerm(string phonySock,string realSock)
        {
            if(Syscall.chmod(
                phonySock,
                //realSock,
                NativeConvert.FromOctalPermissionString ("0666")
            ) < 0)
            {
                //TODO- Use proper logging facilities
                var error = Stdlib.GetLastError();
                Console.WriteLine("Error setting file permissions: " + error);
            }
            if(Syscall.symlink(Path.GetFileName(phonySock),realSock)<0)
            {
                var error = Stdlib.GetLastError();
                Console.WriteLine("Error in creating soft link of the UDS file: "+error);
            }
        }

        private static void DeleteFile(string filename)
        {
            if(File.Exists(filename))
                File.Delete(filename);
        }
    }
}
