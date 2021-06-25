using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FDK
{
    public class ConstructFunc: IConstructFunc
    {
        private Func<string> _function;
        private IHttpContextAccessor _httpContextAccessor;

        public Func<IRequestContext,Func<string>> final_function;
        public ConstructFunc(IHttpContextAccessor httpContextAccessor)
        {
            _function = InvokeClass._userMethod;
            _httpContextAccessor = httpContextAccessor;
            //final_function = NewFunction();
        }

        public Func<IRequestContext,Func<string>> NewFunction()
        {
            IRequestContext ctx = new RequestContext(_httpContextAccessor);
            Func<IRequestContext,Func<string>> output = MethodFunc;
            return output;
        }
        public Func<string> MethodFunc(IRequestContext ctx)
        {
            return _function;
        }
    }
}