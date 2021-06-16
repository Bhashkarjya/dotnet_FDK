using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDK
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //The function handler
            Console.WriteLine(InvokeClass.InvokeHandler());
            //Create the web host
            Server.CreateHostBuilder(args).Build().Run();
        }
    }
}
