using System;
using Microsoft.AspNetCore.Http;

namespace FDK
{
    public static class Utils{
        private const string PREFIX = "Fn-";
        public static IHeaderDictionary GetFnSpecificHeaders(IHeaderDictionary headers){
            //when the header list is empty
            if(headers.ContentLength == 0){
                throw new ArgumentNullException(nameof(headers));
            }
            //We need to return only those headers which starts with the prefix "Fn-Headers"
            IHeaderDictionary FnDictionary = new HeaderDictionary();
            foreach(var item in headers)
            {
                if(item.Key.StartsWith(PREFIX))
                {
                    FnDictionary.Add(item.Key,item.Value);
                }
            }
            return new HeaderDictionary();
        }
    }
}