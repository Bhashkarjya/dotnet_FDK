using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FDK
{
    public class Startup
    {
        public static DateTime startTime;
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>{
                options.AllowSynchronousIO = true;
            });
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddSingleton<IContainerEnvironment,ContainerEnvironment>();
        }

        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env, 
                              ILoggerFactory loggerfactory, 
                              IHostApplicationLifetime applicationLifetime, 
                              IHttpContextAccessor httpContextAccessor, 
                              IContainerEnvironment containerEnvironment
                              )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<ResponseMiddleware>();
            
            applicationLifetime.ApplicationStarted.Register(() => {
                Server.SockPerm(Server._phonySock,Server._realSock);
                startTime = DateTime.Now;
            });

            applicationLifetime.ApplicationStopped.Register(() => 
            {
                //Cleaning up the UDS and the symlink
                File.Delete(Server._realSock);
                File.Delete(Server._phonySock);
            });
        }
    }
}
