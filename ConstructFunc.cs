using System;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace FDK
{
    public class ConstructFunc: IConstructFunc
    {
        private MethodInfo _function;
        private IHttpContextAccessor _httpContextAccessor;

        public Func<IRequestContext,MethodInfo> final_function;
        public ConstructFunc(IHttpContextAccessor httpContextAccessor)
        {
            _function = InvokeClass._userFunction;
            _httpContextAccessor = httpContextAccessor;
            final_function = NewFunction();
        }

        public Func<IRequestContext,MethodInfo> NewFunction()
        {
            IRequestContext ctx = new RequestContext(_httpContextAccessor);
            Func<IRequestContext,MethodInfo> output = MethodFunc;
            return output;
        }
        public MethodInfo MethodFunc(IRequestContext ctx)
        {
            return _function;
        }
    }
}