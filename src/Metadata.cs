using System;
using System.Reflection;

namespace FDK
{
    public class Metadata //currently not in use
    {
        public static string _userNamespace;
        public static string _userClassName;
        public static string _userMethodName;
        public static void GetUserFunctionMetadata(MethodBase info)
        {
            _userNamespace = info.ReflectedType.Namespace;
            _userClassName = info.ReflectedType.Name;
            _userMethodName = info.Name;
        }
    }
}