using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace  FDK
{
    public class ResponseMiddleware
    {
        private Func<IRequestContext,MethodInfo> _function;
        private readonly RequestDelegate _next;
        private IRequestContext _ctx;
        private IHttpContextAccessor _httpContextAccessor;

        public IHeaderDictionary ResponseHeaders{ get; } = new HeaderDictionary();
        public ResponseMiddleware(RequestDelegate next,IHttpContextAccessor httpContextAccessor,
                            IRequestContext ctx, IConstructFunc constructFunc)
        {
            _function = constructFunc.NewFunction();
            _next = next;
            _ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

        //This is the entry point of the middleware. It searches for a method named Invoke or InvokeAsync and implements that method.
        public async Task InvokeAsync(HttpContext context)
        {
            //await context.Response.WriteAsync("Response Body Middleware has been triggered");
            var ExecutionResult = UserFunctionRun();
            var result = ClassifyResult.Create(ExecutionResult);
            await result.WriteResult(context.Response);
            await context.Response.WriteAsync((string)ExecutionResult);
        }

        private object UserFunctionRun()
        {
            var tokenSource = new CancellationTokenSource();
            _ctx.cancellationToken = tokenSource.Token;
            var timeLeft = CalculateTimeLeft();
            if(timeLeft == null)
            {
                _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                return InvokeClass.GetOutput();
            }
            tokenSource.Cancel();
            _httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status504GatewayTimeout;
            return InvokeClass.GetOutput();
        }

        private TimeSpan? CalculateTimeLeft()
        {
            // var deadLine = _ctx.Deadline();
            // if(deadLine == null)
            //     return null;
            // var timeLeft = deadLine - DateTime.Now;
            // if(timeLeft.TotalSeconds < 0)
            //     return null;
            // return timeLeft;
            return null;
        }
    }
}