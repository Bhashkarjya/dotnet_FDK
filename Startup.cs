using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using Mono.Unix.Native;

namespace FDK
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IContainerEnvironment,ContainerEnvironment>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddSingleton<IRequestContext,RequestContext>();
            services.AddSingleton<IConstructFunc,ConstructFunc>();
            Console.WriteLine("Adding services in DI");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerfactory, IHostApplicationLifetime applicationLifetime, IHttpContextAccessor httpContextAccessor, IContainerEnvironment containerEnvironment)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            try{
                var a = httpContextAccessor.HttpContext;
                //Console.WriteLine(a);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Request.Body is a null object");
            }
            //Console.WriteLine("Adding Middlewares");
            
            app.UseMiddleware<ResponseBody>();
            
            applicationLifetime.ApplicationStarted.Register(() => {
                // Logger.CreateLogFile();
                string UnixFilePath = containerEnvironment.FN_LISTENER;
                Console.WriteLine("The Kestrel web server is binded to the "+UnixFilePath);
                string SoftStorageFileOfTheUnixFilePath = containerEnvironment.SYMBOLIC_LINK;
                Syscall.chmod(
                    UnixFilePath,
                    FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IXUSR |
                    FilePermissions.S_IRGRP | FilePermissions.S_IWGRP | FilePermissions.S_IXGRP |
                    FilePermissions.S_IROTH | FilePermissions.S_IWOTH | FilePermissions.S_IXOTH
                );
                Syscall.symlink(UnixFilePath,SoftStorageFileOfTheUnixFilePath);
                Syscall.chmod(
                    SoftStorageFileOfTheUnixFilePath,
                    FilePermissions.S_IRUSR | FilePermissions.S_IWUSR | FilePermissions.S_IXUSR |
                    FilePermissions.S_IRGRP | FilePermissions.S_IWGRP | FilePermissions.S_IXGRP |
                    FilePermissions.S_IROTH | FilePermissions.S_IWOTH | FilePermissions.S_IXOTH
                );
                Console.WriteLine("Unix Socket:" + UnixFilePath);
            });

            applicationLifetime.ApplicationStopped.Register(() => 
            {
                Console.WriteLine("Cleaning the sockets before shutting down the application");
                File.Delete(containerEnvironment.FN_LISTENER);
                File.Delete(containerEnvironment.SYMBOLIC_LINK);
            });
        }
    }
}
