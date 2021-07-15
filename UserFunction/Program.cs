using FDK;
using System;
using System.Reflection;

namespace UserFunction
{
    public class Program
    {
        public static void Main(string[] args)
        {
          try {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            Type functionType = executingAssembly.GetType("UserFunction.JsonExample");
            object functionInstance = Activator.CreateInstance(functionType);
            InvokeClass.HandlerFunc(functionType,functionInstance);
          } catch (Exception e) {
            Console.WriteLine("{0}\n{1}\n{2}", e.ToString(), e.Message, e.Source);
          }
        }
    }
}
