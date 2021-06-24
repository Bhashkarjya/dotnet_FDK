using System;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public class ConstructFunc
    {
        public Func<string> _function;
        private IHttpContextAccessor _httpContextAccessor;

        public Func<IRequestContext,Func<string>> final_function;
        public ConstructFunc(Func<string> function, IHttpContextAccessor httpContextAccessor)
        {
            _function = function;
            _httpContextAccessor = httpContextAccessor;
            final_function = NewFunction();
        }

        private Func<IRequestContext,Func<string>> NewFunction()
        {
            IRequestContext ctx = new RequestContext(_httpContextAccessor);
            Func<IRequestContext,Func<string>> output = MethodFunc;
            return output;
        }
        private Func<string> MethodFunc(IRequestContext ctx)
        {
            return _function;
        }
    }
}