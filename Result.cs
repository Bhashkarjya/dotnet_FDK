using System;
using System.IO;

namespace FDK
{
    public abstract class ClassifyResult{
        public static ConstructResult Create(object result)
        {
            Console.WriteLine(result);
            switch(result)
            {
                case ConstructResult res:
                {
                    Console.WriteLine("FnResult");
                    return res;
                }
                case string str:
                {
                    Console.WriteLine("String");
                    return null;
                }
                case Stream stream:
                {
                    Console.WriteLine("Stream");
                    return null;
                }
                default:
                {
                    Console.WriteLine("JSON");
                    return null;
                }
            }
        }
    }
}