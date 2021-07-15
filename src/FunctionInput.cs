using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace FDK
{
    public class FunctionInput
    {
        private static HttpContext _context;

        private static string _input;
        private static IRequestContext _ctx;

        private static Dictionary<string,string> _dict;
        public FunctionInput(IRequestContext ctx,HttpContext context)
        {
            _ctx = ctx;
            _context = context;
            _input = AsString();
            string ContentType = _ctx.ContentType();
            if(string.IsNullOrEmpty(ContentType))
            {
                Console.WriteLine("Content-Type has not been defined");
            }
            ClassifyInput(ContentType);
        }
        private void ClassifyInput(string ContentType)
        {
            // The input is parsed depending upon the Content-Type of the Request Body.
            // Currently the FDK supports three types of Content-Types: text/plain, application/x-www-form-urlencoded,
            // application/json
            switch(ContentType)
            {
                case("application/json"):
                break;
                case("text/plain"):
                case("application/x-www-form-urlencoded"):
                default:
                _dict = ConvertToDictionary(_input);
                break;
            }
        }

        //Reads the Request body stream and returns a string.
        private string AsString()
        {
            using(var reader = new StreamReader(_context.Request.Body))
            {
                string body = reader.ReadToEnd();
                return body;
            }
        }

        //Getter method to access the Request body as a stream
        public string GetString()
        {
            return _input;
        }

        //Json object is deserialized into a .NET object.
        public T AccessJsonInput<T>() where T : class
        {
            try{
                return JsonConvert.DeserializeObject<T>(_input);
            }
            catch(Exception e)
            {
                // Statement will return an exception if the input format is not a valid JSON object.
                Console.WriteLine(e.Message);
            }
            return null;
        }

        //Deserialization of the XML body. Currently, it is not used since the FDK does not support XML format as of now.
        public List<T> AccessXmlInput<T>() where T : class
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>),new XmlRootAttribute("Employees"));
            using(var reader = new StreamReader(_input))
            {
                return (List<T>)serializer.Deserialize(reader);
            }
        }

        //ConvertToDictionary is called when the input is given in raw data or text/plain format. It splits the different arguments
        // and stores them in a KeyValuePair fashion in a dictionary. The keys are by default named as "arg1" , "arg2" and so on.
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
            return dict;
        }

        // Returns the value stored in the dictionary corresponding to the key given as argument.
        public string GetValue(string argument)
        {
            if(_dict.ContainsKey(argument) == false)
            return "";
            return _dict[argument];
        }
    }
}