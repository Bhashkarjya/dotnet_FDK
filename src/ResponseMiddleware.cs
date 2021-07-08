using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace FDK
{
    public class ResponseMiddleware
    {
        //private Func<IRequestContext,MethodInfo> _function;
        private readonly RequestDelegate _next;
        private IRequestContext _ctx;
        private IHttpContextAccessor _httpContextAccessor;

        public IHeaderDictionary ResponseHeaders{ get; } = new HeaderDictionary();
        public ResponseMiddleware(RequestDelegate next,
                                IHttpContextAccessor httpContextAccessor
                                // IConstructFunc constructFunc
                                )
        {
            // _function = constructFunc.NewFunction();
            _next = next;
            //_ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
            Console.WriteLine("Response Middleware");
        }

        //This is the entry point of the middleware. It searches for a method named Invoke or InvokeAsync and implements that method.
        public async Task InvokeAsync(HttpContext context)
        {
            _ctx = new RequestContext(context);
            Console.WriteLine("Invoke Async Operation");
            //await context.Response.WriteAsync("Response Body Middleware has been triggered");
            var functionExecutionResult = UserFunctionRun();
            //PrintRequestHeaders(context);
            //PrintResponseHeaders(context);
            await ClassifyResult.Create(functionExecutionResult,context);
        }

        private object UserFunctionRun()
        {
            var tokenSource = new CancellationTokenSource();
            _ctx.cancellationToken = tokenSource.Token;
            var timeLeft = CalculateTimeLeft();
            if(timeLeft == null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return InvokeClass.RunUserFunction();
            }
            tokenSource.Cancel();
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            return InvokeClass.RunUserFunction();
        }

        private TimeSpan? CalculateTimeLeft()
        {
            // var deadLine = _ctx.Deadline();
            // Console.WriteLine(DateTime.Now);
            // if(deadLine == null)
            //     return null;
            // var timeLeft = deadLine - DateTime.Now;
            // if(timeLeft.TotalSeconds < 0)
            //     return null;
            // return timeLeft;
            return null;
        }

        private static void PrintRequestHeaders(HttpContext context)
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
            }
            catch{
                Console.WriteLine("Couldnt't catch headers");
            }
        }

        private static void PrintResponseHeaders(HttpContext context)
        {
            Console.WriteLine("**** Response Headers ****");
            try{
                var headers = context.Response.Headers;
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