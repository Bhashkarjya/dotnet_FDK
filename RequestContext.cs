using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public class RequestContext: IRequestContext
    {
        private readonly HttpRequest httpRequest;
        private readonly IHeaderDictionary headers,fn_headers;

        public RequestContext(IHttpContextAccessor contextAccessor){
            httpRequest = contextAccessor.HttpContext.Request;
            headers = contextAccessor.HttpContext.Request.Headers;
            fn_headers = Header(headers);
        }
        public string AppName(){
            return "";
        }

        public string HttpRoute()
        {
            return fn_headers["Fn-Route"];
        }

        public string CallID(){
            return fn_headers["Fn-CallID"];
        }
        public IContainerEnvironment Config(){
            return new ContainerEnvironment();
        }

        public IHeaderDictionary Header(IHeaderDictionary headers){
            return Utils.GetFnSpecificHeaders(headers);
        }

        public string Argument()
        {
            return "";
        }
        public string Format(){
            return fn_headers["Fn-Format"];
        }

        public CancellationToken cancellationToken()
        {
            return new CancellationToken();
        }
        public string ExecutionType()
        {
            return "";
        }

        public string RequestContentType()
        {
            return fn_headers["Fn-Content-Type"];
        }

        public string RequestURL()
        {
            return fn_headers["Fn-RequestURL"];
        }
    }
}
