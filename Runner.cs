using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public static class Runner
    {
        public static string getName()
        {
            return "Charles";
        }
        public static void handle(Func<IRequestContext,string,string> function)
        {
            function(new RequestContext(new HttpContextAccessor()), "Charles");
        }

        public static IPEndPoint getTCPConnectionPoint()
        {
            IPEndPoint endpoint = new IPEndPoint(0x2414188f, 8080);
            return endpoint;
        }
    }
}
