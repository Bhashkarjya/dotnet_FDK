using Microsoft.AspNetCore.Http;
using System;
using System.Collections;

namespace Fnproject.Fn.Fdk.Context
{
    public class RuntimeContext : IRuntimeContext
    {
        private string appID;
        private string appName;
        private string fnID;
        private string fnName;
        private string callID;
        private string fnIntent;

        private DateTime deadline;
        public static readonly IDictionary config = System.Environment.GetEnvironmentVariables();
        private ITracingContext tracingContext;

        private string valueOrEmpty(string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        public RuntimeContext(IHeaderDictionary reqHeaders)
        {
            appID = valueOrEmpty(RuntimeContext.config[Constants.FN_APP_ID] as string);
            appName = valueOrEmpty(RuntimeContext.config[Constants.FN_APP_NAME] as string);
            fnID = valueOrEmpty(RuntimeContext.config[Constants.FN_FN_ID] as string);
            fnName = valueOrEmpty(RuntimeContext.config[Constants.FN_FN_NAME] as string);
            callID = valueOrEmpty(reqHeaders[Constants.FN_CALL_ID_HEADER]);
            fnIntent = valueOrEmpty(reqHeaders[Constants.FN_INTENT_HEADER]);

            if (string.IsNullOrEmpty(reqHeaders[Constants.FN_DEADLINE_HEADER]))
            {
                deadline = DateTime.Now.Add(TimeSpan.FromDays(1));
            }
            else
            {
                deadline = DateTime.Parse(reqHeaders[Constants.FN_DEADLINE_HEADER]);
            }
            tracingContext = new TracingContext(RuntimeContext.config, reqHeaders);
        }
        public string AppID()
        {
            return appID;
        }
        public string FunctionID()
        {
            return fnID;
        }
        public string AppName()
        {
            return appName;
        }
        public string FunctionName()
        {
            return fnName;
        }
        public string CallID()
        {
            return callID;
        }
        public string FnIntent()
        {
            return fnIntent;
        }
        public DateTime Deadline()
        {
            return deadline;
        }
        public string ConfigValueByKey(string key)
        {
            return valueOrEmpty(RuntimeContext.config[key] as string);
        }

        public IDictionary Config()
        {
            return RuntimeContext.config;
        }

        public ITracingContext TracingContext()
        {
            return tracingContext;
        }
    }

}
