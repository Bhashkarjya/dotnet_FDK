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
        string AppName();
        string HttpRoute();
        string CallID();
        IContainerEnvironment Config();
        IHeaderDictionary Header(IHeaderDictionary headers);
        string Argument();
        string Format();
        CancellationToken cancellationToken();
        string ExecutionType();
        string RequestContentType();
        string RequestURL();

    }
}
