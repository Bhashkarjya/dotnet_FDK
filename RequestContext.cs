using System;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDK
{
    public class RequestContext: IRequestContext
    {
        public string AppName(){
            return "";
        }

        public string HttpRoute()
        {
            return "";
        }

        public string CallID(){
            return "";
        }
        public IContainerEnvironment Config(){
            return new ContainerEnvironment();
        }

        public IHeaderDictionary Header(){
            return Utils.GetFnSpecificHeaders();
        }

        public string Argument()
        {
            return "";
        }
        public string Format(){
            return "";
        }

        public CancellationToken cancellationToken()
        {
            return new CancellationToken();
        }
        public string ExecutionType()
        {
            return "";
        }

        public string RequestContentType()
        {
            return "";
        }

        public string RequestURL()
        {
            return "";
        }
    }
}
