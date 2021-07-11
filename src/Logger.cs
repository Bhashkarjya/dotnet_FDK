using System;
using Microsoft.AspNetCore.Http;

namespace FDK.Log
{
    public static class Logger
    {
        public static void logFrameHeader(IHeaderDictionary headers) {
          
          string framer = Environment.GetEnvironmentVariable("FN_LOGFRAME_NAME");
          if (framer.Length == 0) {
            return;
          }

          string valueSrc = Environment.GetEnvironmentVariable("FN_LOGFRAME_HDR");
          if (valueSrc.Length == 0) {
            return;
          }

          string id = headers[valueSrc];
          if (id.Length != 0) {
            Console.WriteLine("\n{0}={1}", framer, id);
            Console.Error.WriteLine("\n{0}={1}", framer, id);
          }
        }
    }
}
