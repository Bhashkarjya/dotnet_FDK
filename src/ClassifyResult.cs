using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public abstract class ClassifyResult{
        public static async Task Create(object result, HttpContext context)
        {

            if(result.GetType() == typeof(string))
            {
                //Console.WriteLine("Success");
                string str = (string)result;
                ConstructResult res = new RawResult(str);
                await res.WriteResult(context.Response);

            }
            else if(result.GetType() == typeof(ConstructResult))
            {
                ConstructResult res = (ConstructResult)result;
                await res.WriteResult(context.Response);
            }
            else if(result.GetType() == typeof(Stream))
            {
                Stream stream = (Stream)result;
                ConstructResult res = new StreamResult(stream);
                await res.WriteResult(context.Response);
            }
            else
            {
                JsonResult res = new JsonResult(result);
                await res.WriteResult(context.Response);
            }
        }
    }
}