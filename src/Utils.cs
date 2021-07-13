using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace FDK
{
    public static class Utils{
        private const string PREFIX = "Fn-";
        public static IHeaderDictionary GetFnSpecificHeaders(IHeaderDictionary headers){
            IHeaderDictionary FnDictionary = new HeaderDictionary();
            foreach(var item in headers)
            {
                if(item.Key.StartsWith(PREFIX))
                {
                    FnDictionary.Add(item.Key,item.Value);
                }
            }
            return FnDictionary;
        }

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


