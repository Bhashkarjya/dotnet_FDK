using System;
using System.Collections.Generic;
using FDK;
namespace UserFunction
{
    //Example User Function to showcase how to access inputs given in the json format.
    public class XmlExample
    {
        //Rules to be followed while defining your function.
        // 1. Only your main function should be declared as public and rest of the helper functions should be declared as non-public.
        // 2. The user has the liberty of defining the function as static or non-static.
        // 3. The number of parameters of the user function is constant, i.e 2.
        // 4. The "context" argument contains all the request headers and the container environment details.
        // 5. The "input" argument is pulled out from the request used to trigger a function.
        // 6. How to access the input arguments with different types of Content-Types?
        // (i) text/plain and application/x-www-url-encodeded format - Since the request body doesn't have a key-value pair, the key names
        //     have been by default names as arg1,arg2 and so on. For example, Request.body = "Kevin 18 Oracle", in order to access "Kevin",
        //     input.GetValue("arg1"). Similarly to access "18", use input.GetValue("arg2") and so on.
        // (ii) application/json - Define a class so that the Json object can be deserialized into the .NET object. Use - 
        //      T obj = input.AccessJsonInput<T>(); to get an instance of the defined class with the attributes populated by the input
        //      from the Json object.
        public static string HelloWorld(IRequestContext context, FunctionInput input)
        {
            string name = "";
            int age = 0;
            try{
                List<Employee> employee = input.AccessXmlInput<Employee>();
                name = employee[0].Name;
                age = employee[0].Age;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            if(string.IsNullOrEmpty(name) == false)
                return ("Hello, I'm " + name + "\n I'm " + age);
            string str = "Hello World";
            return str;
        }
    }

    public class Employee{
        public string Name{get; set;}
        public int Age {get; set;}
    }
}
