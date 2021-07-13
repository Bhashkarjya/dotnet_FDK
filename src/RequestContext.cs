using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public class RequestContext: IRequestContext
    {
        private readonly HttpRequest _httpRequest;
        private readonly IHeaderDictionary headers, fn_headers;

        public RequestContext(HttpContext context){
            try{
                _httpRequest = context.Request;
                headers = _httpRequest.Headers;
                fn_headers = Utils.GetFnSpecificHeaders(headers);
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine("Message: {0}, Source: {1}", e.Message, e.Source);
            }
            
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
            // if(fn_headers.Contains("Fn-Deadline"))
            if(String.IsNullOrEmpty(fn_headers["Fn-Deadline"]))
            {
                DateTime date = new DateTime(1000,1,1);
                return date;
            }
            return DateTime.Parse(fn_headers["Fn-Deadline"]);
        }

        public string Intent()
        {
            return fn_headers["Fn-Intent"];
            //values will be application/cloudevent or application/json
        }
    }
}
