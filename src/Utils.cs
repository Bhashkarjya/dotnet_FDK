using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace FDK
{
    public static class Utils{
        private const string PREFIX = "Fn-";
        public static IHeaderDictionary GetFnSpecificHeaders(IHeaderDictionary headers){
            //when the header list is empty
            // if(headers.ContentLength == 0){
            //     throw new ArgumentNullException(nameof(headers));
            // }
            //We need to return only those headers which starts with the prefix "Fn-Headers"
            IHeaderDictionary FnDictionary = new HeaderDictionary();
            foreach(var item in headers)
            {
                Console.WriteLine(item.Key+":"+item.Value);
                if(item.Key.StartsWith(PREFIX))
                {
                    FnDictionary.Add(item.Key,item.Value);
                }
            }
            return FnDictionary;
        }

        public static IPEndPoint getTCPConnectionPoint()
        {
            IPEndPoint endpoint = new IPEndPoint(0x2414188f, 8080);
            return endpoint;
        }

        // public static void handle(Func<IRequestContext,string,string> function)
        // {
        //     function(new RequestContext(new IHttpContext()), "Charles");
        // }

        public static void ExecuteShellScript()
        {
            string filename = "./script.sh";
            var arguments = "";
            ProcessStartInfo proc = new ProcessStartInfo()
            {
                FileName = filename,
                Arguments = arguments,
                UseShellExecute=false,
                RedirectStandardOutput= true,
                CreateNoWindow = true

            };
            Process process = Process.Start(proc);
            while(!process.StandardOutput.EndOfStream)
            {
                string result = process.StandardOutput.ReadLine();
            }
            process.WaitForExit();
        }
    }
}


