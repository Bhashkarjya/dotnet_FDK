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
        //public static Func<string> _userMethod;
        public static MethodInfo _userMethod;
        public static Type _functionType;
        public static object _functionInstance;  
        public static void HandlerFunc(Type functionType, object functionInstance,MethodInfo userMethod)
        {
            //This is the entry point where the user function will first enter the FDK
            _userMethod = userMethod;
            _functionInstance = functionInstance;
            _functionType = functionType;
            string[] parameters = new string[0];
            //Console.WriteLine(_userMethod.Invoke(_functionInstance,parameters));
            Server.CreateHostBuilder(new ContainerEnvironment()).Build().Run();
            //return output;
        }

        //Call this function to get the user function code
    }
}
