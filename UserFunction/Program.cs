using FDK;
using System;
using System.Reflection;

namespace UserFunction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Type FunctionType = executingAssembly.GetType("UserFunction.Example");
            object FunctionInstance = Activator.CreateInstance(FunctionType);
            MethodInfo UserMethod = FunctionType.GetMethod("HelloWorld");
            //string[] parameters = new string[0];
            //object output = UserMethod.Invoke(FunctionInstance,parameters);
            InvokeClass.HandlerFunc(FunctionType,FunctionInstance,UserMethod);

            //Previously Invoke was handled in this manner
            //InvokeClass.HandlerFunc(Example.HelloWorld);
        }
    }
}
