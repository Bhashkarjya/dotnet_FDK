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
            //services.AddScoped<IRequestContext,RequestContext>();
            //services.AddSingleton<IConstructFunc,ConstructFunc>();
            //Console.WriteLine("Adding services in DI");
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
                CreateHttpRequest.HttpRequestCreation(containerEnvironment);
            });

            applicationLifetime.ApplicationStopped.Register(() => 
            {
                Console.WriteLine("Cleaning the sockets before shutting down the application");
 //               File.Delete(containerEnvironment.FN_LISTENER);
  //              File.Delete(containerEnvironment.SYMBOLIC_LINK);
            });
        }
    }
}
