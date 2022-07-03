using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace UMH_STUDENT_SERVICE.Utils
{
    public class CustomJsonResult
    {
        public string Error { set; get; }
        public string UrlRedirect { set; get; }
        public object Result { set; get; }
        public string Exception { set; get; }
        public string TypeResult { set; get; }
        public string SuccesMessage { set; get; }
        public string InfoMessage { set; get; }

        public CustomJsonResult()
        {

        }

        public CustomJsonResult(string error)
        {
            this.Error = error;
        }

        public CustomJsonResult(object result)
        {
            this.Result = result;
        }

        public CustomJsonResult(string error, string urlRedirect, object result, string exception, string typeResult, string succesMessage, string infoMessage)
        {
            Error = error;
            UrlRedirect = urlRedirect;
            Result = result;
            Exception = exception;
            TypeResult = typeResult;
            SuccesMessage = succesMessage;
            InfoMessage = infoMessage;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
