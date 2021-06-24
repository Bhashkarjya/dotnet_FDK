using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace  FDK
{
    public class ResponseBody
    {
        private Func<IRequestContext,Func<string>> _function;
        private readonly RequestDelegate _next;
        public ResponseBody(RequestDelegate next,Func<IRequestContext,Func<string>> function)
        {
            _function = function;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //await Console.WriteLine("This is the entry point for the Middleware");
            Console.WriteLine("Hello Invoke");
            // string str = "Hello Invoke";
            // Stream writer = 
            // writer.CopyToAsync(context.Response.Body);


            await _next(context);
        }
    }
}