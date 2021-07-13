using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FDK
{
    public class FunctionInput
    {
        private static HttpContext _context;
        private static IRequestContext _ctx;

        private static Dictionary<string,string> _dict;
        public FunctionInput(IRequestContext ctx,HttpContext context)
        {
            _ctx = ctx;
            _context = context;
            string input = ClassifyInput(_context);
            string ContentType = _ctx.ContentType();
            _dict = ConvertToDictionary(input);
            // switch(ContentType)
            // {
            //     case("text/plain"):
            //     case("application/x-www-form-urlencoded"):
            //     _dict = ConvertToDictionary(input);
            //     break;
            //     default:
            //     break;
            // }
        }
        // The input is parsed depending upon the Content-Type of the Request Body.
        // Currently the FDK supports three types of Content-Types: text/plain, application/json, application/xml
        public string ClassifyInput(HttpContext context)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                string body = reader.ReadToEnd();
                return body;
            }
            // string contentType = _ctx.ContentType();
            // switch(contentType)
            // {
            //     case("text/plain"):
            //     case("application/x-www-form-urlencoded"):
            //         using (var reader = new StreamReader(context.Request.Body))
            //         {
            //             string body = reader.ReadToEnd();
            //             return body;
            //         }
            //     case("application/json"):
            //         using(var reader = new StreamReader(context.Request.Body))
            //         {
            //             string body = reader.ReadToEnd();
            //             return body;
            //         }
            //     case("application/xml"):
            //     using(var reader = new StreamReader(context.Request.Body))
            //     {
            //         string body = reader.ReadToEnd();
            //         return body;
            //     }
            //     default:
            //         return "";
            // }
        }

        private string ConvertToJsonString(string body)
        {
            char [] delimiters = {',',' ','(',')','\'' };
            string[] arguments = body.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            string jsonString = ConstructJsonString(arguments);
            return jsonString;
        }

        private string ConstructJsonString(string[] arguments)
        {
            string jsonString = "{";
            int argCount = 1;
            foreach(string arg in arguments)
            {
                jsonString=jsonString+"arg"+argCount.ToString()+": "+arg+", ";
                argCount++;
            }
            jsonString = jsonString.Remove(jsonString.Length-2);
            return jsonString+"}";
        }

        private Dictionary<string,string> ConvertToDictionary(string input)
        {
            char [] delimiters = {',',' ','(',')','\'' };
            string[] arguments = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string,string> dict = new Dictionary<string, string>();
            int argCount = 0;
            foreach(string arg in arguments)
            {
                argCount++;
                dict.Add("arg"+argCount.ToString(),arg);
            }
            foreach(KeyValuePair<string,string>  element in dict)
            {
                Console.WriteLine("{0} : {1}",element.Key, element.Value);
            }
            return dict;
        }

        public string GetValue(string argument)
        {
            if(_dict.ContainsKey(argument) == false)
            return "";
            return _dict[argument];
        }
    }
}