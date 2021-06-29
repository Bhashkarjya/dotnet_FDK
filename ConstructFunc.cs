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
            _function = InvokeClass._userMethod;
            Console.WriteLine("Construct Function: ");
            Console.WriteLine(_function.Invoke(InvokeClass._functionInstance,new string[0]));
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