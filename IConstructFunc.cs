using System;

namespace FDK
{
    public interface IConstructFunc
    {
        Func<IRequestContext,Func<string>> NewFunction();

        Func<string> MethodFunc(IRequestContext ctx);
    }
}