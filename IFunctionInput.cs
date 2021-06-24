using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace  FDK
{
    public interface IFunctionInput
    {
        string InputString();

        Stream InputStream();
    }
}