using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FDK
{
    public class ResponseMiddleware
    {
        //private Func<IRequestContext,MethodInfo> _function;
        private readonly RequestDelegate _next;
        private IRequestContext _ctx;
        private IHttpContextAccessor _httpContextAccessor;
        private FunctionInput _input;

        public IHeaderDictionary ResponseHeaders{ get; } = new HeaderDictionary();
        public ResponseMiddleware(RequestDelegate next,
                                IHttpContextAccessor httpContextAccessor
                                // IConstructFunc constructFunc
                                )
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        //This is the entry point of the middleware. It searches for a method named Invoke or InvokeAsync and implements that method.
        public async Task InvokeAsync(HttpContext context)
        {
            _ctx = new RequestContext(context);
            _input = new FunctionInput(_ctx,context);
            var functionExecutionResult = UserFunctionRun();
            PrintRequestHeaders(context);
            await ClassifyResult.Create(functionExecutionResult,context,_ctx);
        }

        private object UserFunctionRun()
        {
            var tokenSource = new CancellationTokenSource();
            _ctx.cancellationToken = tokenSource.Token;
            var timeLeft = CalculateTimeLeft();
            if(timeLeft == null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
                return InvokeClass.RunUserFunction(_ctx,_input);
            }
            tokenSource.Cancel();
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            return InvokeClass.RunUserFunction(_ctx,_input);
        }

        private double? CalculateTimeLeft()
        {
            var deadLine = _ctx.Deadline();
            //When you are running locally, the request header won't inject the Fn-Deadline, so we are taking a hard coded DateTime value.
            DateTime date1 = new DateTime(1000,1,1);
            if(deadLine == date1)
                return null;
            if(deadLine == null)
                return null;
            var timeLeft = (deadLine - DateTime.Now).TotalSeconds;
            if(timeLeft < 0)
                return null;
            return timeLeft;
        }

        public static void PrintRequestHeaders(HttpContext context)
        {
            Console.WriteLine("****Request Headers ****");
            try{
                var headers = context.Request.Headers;
                int header_count = 0;
                foreach(var h in headers)
                {
                    header_count++;
                    Console.WriteLine(header_count+")" +  h.Key+":"+h.Value);
                }
                try{
                }
                catch(Exception e)
                {
                    Console.WriteLine("{0} {1}",e.Source,e.Message);
                }
            }
            catch{
                Console.WriteLine("Couldnt't catch headers");
            }
        }

        public static void PrintResponseHeaders(HttpResponse context)
        {
            Console.WriteLine("**** Response Headers ****");
            try{
                var headers = context.Headers;
                int header_count = 0;
                foreach(var h in headers)
                {
                    header_count++;
                    Console.WriteLine(header_count+")" + h.Key+":"+h.Value);
                }
            }
            catch{
                Console.WriteLine("Couldn't catch Response headers");
            }
        }
    }
}