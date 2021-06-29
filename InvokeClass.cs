using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection;

namespace FDK
{
    public class InvokeClass
    {
        public static IServiceCollection services;

        public static Type _functionType;
        public static object _functionInstance;

        public static MethodInfo _userFunction;
        public static void HandlerFunc(Type functionType, object functionInstance, MethodInfo userFunction)
        {
            //This is the entry point where the user function will first enter the FDK
            _functionType = functionType;
            _functionInstance = functionInstance;
            _userFunction = userFunction;
            var output = userFunction.Invoke(functionInstance, new string[0]);
            Console.WriteLine("Reflection: {0}",output);
            Server.CreateHostBuilder(new ContainerEnvironment()).Build().Run();
            //return output;
        }

        //Call this function to get the user function code
    }
}