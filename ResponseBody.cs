using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading;

namespace  FDK
{
    public class ResponseBody
    {
        private Func<IRequestContext,Func<string>> _function;
        private readonly RequestDelegate _next;
        private IRequestContext _ctx;
        private IHttpContextAccessor _httpContextAccessor;

        public IHeaderDictionary ResponseHeaders{get; } = new HeaderDictionary();
        public ResponseBody(RequestDelegate next,IHttpContextAccessor httpContextAccessor,IRequestContext ctx)
        {
            ConstructFunc obj = new ConstructFunc(httpContextAccessor);
            //_function = obj.final_function;
            _next = next;
            _ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Invoke(HttpContext context)
        {
            Console.WriteLine("This middleware is triggered when we invoke the endpoint");
            context.Response.WriteAsync("Response Body Middleware has been triggered");
            var ExecutionResult = UserFunctionRun();
            
        }

        public void CreateResponseBody(HttpResponse response)
        {
            if(response.StatusCode!= StatusCodes.Status200OK)
            {
                response.Headers["Fn-Http-Status"] = StatusCodes.Status200OK.ToString();
            }
            foreach(var header in ResponseHeaders)
            {
                response.Headers["Fn-Http-H-" + header.Key] = header.Value; 
            }

            response.WriteAsync("User's output");
        }

        private object UserFunctionRun()
        {
            var tokenSource = new CancellationTokenSource();
            _ctx.cancellationToken = tokenSource.Token;
            var timeLeft = CalculateTimeLeft();
            if(timeLeft == null)
            {
                var output = _function(_ctx);
                Console.WriteLine(output);
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return output;
            }
            tokenSource.Cancel();
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            return _function(_ctx);
        }

        private TimeSpan? CalculateTimeLeft()
        {
            var deadLine = _ctx.Deadline();
            if(deadLine == null)
                return null;
            var timeLeft = deadLine - DateTime.Now;
            if(timeLeft.TotalSeconds < 0)
                return null;
            return timeLeft;
        }
    }
}