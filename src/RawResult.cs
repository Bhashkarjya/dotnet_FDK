using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

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
        private object _result;

        public JsonSerializerSettings jsonSerializerSettings{get; set;}
        public JsonResult(object result)
        {
            _result = result;
            ContentType = "application/json";
        }
        public override async Task WriteResultBody(HttpResponse response)
        {
            using (var writer = new HttpResponseStreamWriter(response.Body,Encoding))
            {
                var jsonSerializer = JsonSerializer.Create(jsonSerializerSettings);
                jsonSerializer.Serialize(writer,_result);
                await writer.FlushAsync();
            }
        }
    }
}