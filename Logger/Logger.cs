using System;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FDK.Log
{
    public static class Logger
    {
        private static string _filepath = "./Logging/ErrorLog.txt";
        private static StreamWriter writer;

        public static void EnableFromHeaders(IHeaderDictionary headers) {
          
          string framer = Environment.GetEnvironmentVariable("FN_LOGFRAME_NAME");
          if (framer.Length == 0) {
            return;
          }

          string valueSrc = Environment.GetEnvironmentVariable("FN_LOGFRAME_HDR");
          if (valueSrc.Length == 0) {
            return;
          }

          string framerHeaderValue = headers[valueSrc];
          if (framerHeaderValue.Length != 0) {
            Console.WriteLine("\n{0}={1}", framer, framerHeaderValue);
            Console.Error.WriteLine("\n{0}={1}", framer, framerHeaderValue);
          }
        }

        public static void CreateLogFile()
        {
            if(File.Exists(_filepath))
            {
                File.WriteAllText(_filepath,String.Empty);
            }
            else
            {
                using (FileStream fs = File.Create(_filepath))
                {}
            }
            StringBuilder str = new StringBuilder();
            writer = new StreamWriter(_filepath,true);
            str.Append("Log File Created");
            WriteToFile(str);
        }

        public static void WriteToFile(StringBuilder str)
        {
            writer.WriteLine(str.ToString());
            writer.Flush();
        }

        public static void BuildTheLogFile(Exception ex)
        {
            StringBuilder str = new StringBuilder();
            str.Append("***************" + "Error Log - " + DateTime.Now + "****************");
            str.Append(Environment.NewLine);
            str.Append("Exception Type:" + ex.GetType().Name);
            str.Append(Environment.NewLine);
            str.Append("Error Message:" + ex.Message);
            str.Append(Environment.NewLine);
            str.Append("Error Source:" + ex.Source);
            str.Append(Environment.NewLine);
            if(ex.StackTrace != null)
            {
                str.Append("Error Trace:" + ex.StackTrace);
            }
            Exception InnerEx = ex.InnerException;
            while(InnerEx != null)
            {
                str.Append(Environment.NewLine);
                str.Append("Exception Type:" + InnerEx.GetType().Name);
                str.Append(Environment.NewLine);
                str.Append("Error Message:" + InnerEx.Message);
                str.Append(Environment.NewLine);
                str.Append("Error Source:" + ex.Source);
                str.Append(Environment.NewLine);
                if(InnerEx.StackTrace != null)
                {
                    str.Append("Error Trace:" + InnerEx.StackTrace);
                }
                InnerEx = InnerEx.InnerException;
            }
            if(File.Exists(_filepath))
            {
                WriteToFile(str);
            }
        }

        public static void CloseWriter()
        {
            writer.Flush();
            writer.Close();
        }
    }
}
