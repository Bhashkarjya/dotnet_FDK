using System;
using System.Reflection;

namespace FDK
{
    public interface IConstructFunc
    {
        Func<IRequestContext,MethodInfo> NewFunction();

        MethodInfo MethodFunc(IRequestContext ctx);
    }
}