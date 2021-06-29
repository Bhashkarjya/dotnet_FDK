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
            InvokeClass.HandlerFunc(Example.HelloWorld);
          } catch (Exception e) {
            Console.WriteLine("{0}\n{1}\n{2}", e.ToString(), e.Message, e.Source);
          }
        }
    }
}
