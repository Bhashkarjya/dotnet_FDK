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
        private Func<IRequestContext,Func<string>> _function;
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IContainerEnvironment,ContainerEnvironment>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            //services.AddSingleton<IRequestContext,RequestContext>();
            Console.WriteLine("Adding services in DI");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerfactory, IHostApplicationLifetime applicationLifetime, IHttpContextAccessor httpContextAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            try{
                var a = httpContextAccessor.HttpContext;
                Console.WriteLine(a);
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Request.Body is a null object");
            }
            Console.WriteLine("Adding Middlewares");
            ConstructFunc obj = new ConstructFunc(InvokeClass._userMethod,httpContextAccessor);
            _function = obj.final_function;

            app.UseMiddleware<ResponseBody>(_function);
            
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

            applicationLifetime.ApplicationStopped.Register(() => 
            {
                Console.WriteLine("Cleaning the sockets before shutting down the application");
                File.Delete(new ContainerEnvironment().FN_LISTENER);
                File.Delete(new ContainerEnvironment().SYMBOLIC_LINK);
            });
        }
    }
}
