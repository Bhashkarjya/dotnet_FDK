using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using System.Threading;

namespace FDK
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddSingleton<IContainerEnvironment,ContainerEnvironment>();
        }

        public void Configure(IApplicationBuilder app, 
                              IWebHostEnvironment env, 
                              ILoggerFactory loggerfactory, 
                              IHostApplicationLifetime applicationLifetime, 
                              IHttpContextAccessor httpContextAccessor, 
                              IContainerEnvironment containerEnvironment
                            //   IConstructFunc constructFunc
                              )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<ResponseMiddleware>();
            
            applicationLifetime.ApplicationStarted.Register(() => {
                Console.WriteLine("The application is starting");
                Server.SockPerm(Server._phonySock,Server._realSock);
                //CreateHttpRequest.HttpRequestCreation();
            });

            applicationLifetime.ApplicationStopped.Register(() => 
            {
                Console.WriteLine("The application is stopped");
                //File.Delete(Server._phonySock);
                File.Delete(Server._realSock);
            });
        }
    }
}
