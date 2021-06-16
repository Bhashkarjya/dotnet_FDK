using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public interface IRequestContext
    {
        //Fn_Intent specifies how the caller intends the input message to be processed
        string AppName();
        string HttpRoute();
        string CallID();
        IContainerEnvironment Config();
        IHeaderDictionary Header();
        string Argument();
        string Format();
        CancellationToken cancellationToken();
        string ExecutionType();
        string RequestContentType();
        string RequestURL();

    }
}
