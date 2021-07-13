using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Threading.Tasks;

namespace FDK
{
    public abstract class ConstructResult
    {
        public string ContentType{get; set;} = "text/plain";

        public Encoding Encoding{get; set;} = Encoding.UTF8;

        public IHeaderDictionary ResponseHeaders{get; } = new HeaderDictionary();

        public int HttpStatus {get; set;} = StatusCodes.Status200OK;

        public async Task WriteResult(HttpResponse response, IRequestContext ctx)
        {
            response.ContentType = ctx.ContentType();
            response.Headers["Fn-Http-Status"] = HttpStatus.ToString();
            foreach(var header in ResponseHeaders)
            {
                response.Headers["Fn-Http-H-" + header.Key] = header.Value;
            }
            ResponseMiddleware.PrintResponseHeaders(response);
            await WriteResultBody(response);
        }

        public abstract Task WriteResultBody(HttpResponse response);
    }
}