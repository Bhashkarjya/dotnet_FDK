using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDK
{
    public static class Example
    {
        public static string HelloWorld(IRequestContext ctx)
        {
            return "Hello Everyone";
        }

        public static string HelloWorldWithParams(string input)
        {
            return ($"Hello {input}");
        }
    }
}
