using System;
using System.IO;
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
        private Stream _stream;

        public StreamResult(Stream result)
        {
            _stream = result;
        }
        public override async Task WriteResultBody(HttpResponse response)
        {
            await _stream.CopyToAsync(response.Body);
            _stream.Dispose();
        }
    }

    public class JsonResult : ConstructResult
    {
        public override async Task WriteResultBody(HttpResponse response)
        {

        }
    }
}