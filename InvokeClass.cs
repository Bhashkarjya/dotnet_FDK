using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace FDK
{
    public class InvokeClass
    {
        public static IServiceCollection services;
        private static string functionName;
        public static Type _functionType;
        public static object _functionInstance;

        public static MethodInfo _userFunction;

        public static ParameterInfo[] _parameters;
        public static ParameterInfo _returnType;
        public static void HandlerFunc(Type functionType, object functionInstance)
        {
            //This is the entry point where the user function will first enter the FDK
            _functionType = functionType;
            //Getting an array of methods
            MethodInfo[] methodInfos =  _functionType.GetMethods();
            foreach(MethodInfo val in methodInfos)
            {
                //We only want the name of the user-defined method
                if(val.Name != "GetType" && val.Name!= "ToString" && val.Name!="Equals" && val.Name!="GetHashCode")
                {
                    functionName = val.Name;
                    break;
                }
            }
            _functionInstance = functionInstance;
            //get the user's function
            _userFunction = _functionType.GetMethod(functionName);
            //get the array of parameters
            _parameters = _userFunction.GetParameters();
            //get the return type of the user's function
            _returnType = _userFunction.ReturnParameter;
            //this is how we invoke the function
            var output = _userFunction.Invoke(functionInstance, null);
            Console.WriteLine(_parameters.Length);
            Console.WriteLine("Reflection: {0}",output);
            Server.CreateHostBuilder(new ContainerEnvironment()).Build().Run();
        }

        //Call this function to get the user function code
        public static object GetOutput()
        {
            if(_parameters.Length==0)
            {
                return _userFunction.Invoke(_functionInstance,null);
            }
            else
            {
                object[] listOfParameters = new object[_parameters.Length];
                for(int i = 0; i < _parameters.Length; i++)
                {
                    // Assign the parameters to the listOfParameters array
                }
                return _userFunction.Invoke(_functionInstance,listOfParameters);
            }
        }
    }
}