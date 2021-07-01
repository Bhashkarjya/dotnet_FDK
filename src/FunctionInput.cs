using System;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace FDK
{
    public class FunctionInput: IFunctionInput
    {
        private HttpContextAccessor _contextAccessor;
        public FunctionInput(HttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public string InputString()
        {
            using (StreamReader reader = new StreamReader(_contextAccessor.HttpContext.Request.Body))
            {
                return reader.ReadToEnd();
            }
        }

        public Stream InputStream()
        {
            return _contextAccessor.HttpContext.Request.Body;
        }
    }
}