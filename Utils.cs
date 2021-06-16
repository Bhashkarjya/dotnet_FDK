using System;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public static class Utils{
        public static IHeaderDictionary GetFnSpecificHeaders(){
            return new HeaderDictionary();
        }
    }
}