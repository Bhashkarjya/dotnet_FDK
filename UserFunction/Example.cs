using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FDK;

namespace UserFunction
{
    public class Example
    {
        public static string HelloWorld()
        {
            return "Hello Everyone";
        }

        public static string HelloWorldWithParams(string input)
        {
            return ($"Hello {input}");
        }
    }
}
