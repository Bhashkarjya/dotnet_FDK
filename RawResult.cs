using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public class RawResult : ConstructResult
    {
        private string _res;
        public RawResult(string res)
        {
            _res = res;
        }
        public override async Task WriteResultBody(HttpResponse response)
        {
            await response.WriteAsync(_res,Encoding);
        }
    }

    public class StreamResult : ConstructResult
    {
        public override async Task WriteResultBody(HttpResponse response)
        {

        }
    }

    public class JsonResult : ConstructResult
    {
        public override async Task WriteResultBody(HttpResponse response)
        {

        }
    }
}