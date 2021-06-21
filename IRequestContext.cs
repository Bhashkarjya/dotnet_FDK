using System;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public interface IRequestContext
    {
        string Intent();
        string CallID();
        IContainerEnvironment Config();
        IHeaderDictionary Header(IHeaderDictionary headers);
        string Format();
        CancellationToken cancellationToken();
        DateTime Deadline();
        string ExecutionType();
        string ContentType();
        string RequestURL();

    }
}
