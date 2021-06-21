using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using Mono.Unix.Native;
using Logging;

namespace FDK
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //adding services to the Dependency Injection Container
            services.AddSingleton<IContainerEnvironment,ContainerEnvironment>();
            //services.AddSingleton<IRequestContext,RequestContext>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
        }

        // This method gets called by the runtime.Adding the middlewares in the HTTP pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerfactory, IHostApplicationLifetime applicationLifetime)
        {
            //Is the loggerfactory used for logging purposes??
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //This chunk of code gets implemented as soon as the app starts
            //Binding the Kestrel web server to the UDS
            applicationLifetime.ApplicationStarted.Register(() => {
                LogFile.CreateLogFile();
                string UnixFilePath = new ContainerEnvironment().FN_LISTENER;
                Console.WriteLine("The Kestrel web server is binded to the "+UnixFilePath);
                string SoftStorageFileOfTheUnixFilePath = new ContainerEnvironment().SYMBOLIC_LINK;
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

            //This chunk of code is implemented when the app closes
            //deleting the unix socket path when the application is closed
            applicationLifetime.ApplicationStopped.Register(() => 
            {
                Console.WriteLine("Cleaning the sockets before shutting down the application");
                File.Delete(new ContainerEnvironment().FN_LISTENER);
                File.Delete(new ContainerEnvironment().SYMBOLIC_LINK);
                //LogFile.CloseWriter();
            });
        }
    }
}
