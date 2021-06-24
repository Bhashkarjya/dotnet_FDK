using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace FDK
{
    public class InvokeClass
    {
        public static IServiceCollection services;
        public static Func<string> _userMethod;
        public static void HandlerFunc(Func<string> userMethod)
        {
            //This is the entry point where the user function will first enter the FDK
            string output = userMethod();
            Console.WriteLine(output);
            _userMethod = userMethod;
            Server.CreateHostBuilder(new ContainerEnvironment()).Build().Run();
            //return output;
        }

        //Call this function to get the user function code
    }
}
