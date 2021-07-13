using FDK;
using System;
namespace UserFunction
{
    public class Example
    {
        //Rules to be followed while defining your function.
        // 1. Only your main function should be declared as public and rest of the helper functions should be declared as non-public.
        // 2. The user has the liberty of defining the function as static or non-static.
        // 3. The number of parameters of the user function is constant, i.e 2.
        // 4. The "context" argument contains all the request headers and the container environment details.
        // 5. The "input" argument is pulled out from the request used to trigger a function.
        public static string HelloWorld(IRequestContext context, FunctionInput input)
        {
            string name = "";
            name = input.GetValue("arg1");
            if(string.IsNullOrEmpty(name) == false)
            {
                return ("Hello " + name); 
            }
            string str = "Hello World";
            return str;
        }
    }
}
