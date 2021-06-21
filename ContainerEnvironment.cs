using System;

namespace FDK
{
    public class ContainerEnvironment: IContainerEnvironment
    {
        public string FN_ID {get; set; } = Environment.GetEnvironmentVariable("FN_ID");
        public string FN_NAME{get; set; } = Environment.GetEnvironmentVariable("FN_NAME");
        public string FN_APP_ID{get; set;} = Environment.GetEnvironmentVariable("FN_APP_ID");
        public string FN_APP_NAME{get; set;} = Environment.GetEnvironmentVariable("FN_APP_NAME");

        public string FN_FORMAT{get; set;} = Environment.GetEnvironmentVariable("FN_FORMAT");
        public string FN_LISTENER { get; set; } = Environment.GetEnvironmentVariable("FN_LISTENER").Substring(5);
        
        public Int32 FN_MEMORY { get; set; } = int.Parse(Environment.GetEnvironmentVariable("FN_MEMORY"));
        public Int32 FN_TMPSIZE { get; set; } = int.Parse(Environment.GetEnvironmentVariable("FN_TMPSIZE"));
        public string SOCKET_TYPE{get; set;} = "Unix";

        public string SYMBOLIC_LINK{get; set;} = Environment.GetEnvironmentVariable("SYMLINK");
    }
}
