using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public class RequestContext: IRequestContext
    {
        private readonly HttpRequest httpRequest;
        private readonly IHeaderDictionary headers, fn_headers;

        public RequestContext(IHttpContextAccessor contextAccessor){
            httpRequest = contextAccessor.HttpContext.Request;
            headers = contextAccessor.HttpContext.Request.Headers;

            // Start logging this request from environment variables and headers
            FDK.Log.Logger.EnableFromHeaders(headers);

            fn_headers = Header(headers);
        }

        public string CallID(){
            return fn_headers["Fn-Call-Id"];
        }
        public IContainerEnvironment Config(){
            return new ContainerEnvironment();
        }

        public IHeaderDictionary Header(IHeaderDictionary headers){
            return Utils.GetFnSpecificHeaders(headers);
        }
        public string Format(){
            return fn_headers["Fn-Format"];
        }

        public CancellationToken cancellationToken{get; set;}
        public string ExecutionType()
        {
            return "";
        }

        public string ContentType()
        {
            return headers["Content-Type"];
        }

        public string RequestURL()
        {
            return fn_headers["Fn-Http-Request-Url"];
        }

        public DateTime Deadline()
        {
            return DateTime.Parse(fn_headers["Fn-Deadline"]);
        }

        public string Intent()
        {
            return fn_headers["Fn-Intent"];
            //values will be application/cloudevent or application/json
        }
    }
}
