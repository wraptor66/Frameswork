using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace Programming.MethodRouting
{

    public class MethodRouting
    {
        public dynamic ProcessRequest(string JsonPayload)
        {
            try
            {
                JObject jObject = JObject.Parse(JsonPayload);
                string methodName = jObject["methodname"].ToString();
                Programming.RequestProcessing requestProcessing = new RequestProcessing();
                MethodInfo methodInfo = requestProcessing.GetType().GetMethod(methodName);
                object parameterValue = JsonPayload;
                var returnObject = methodInfo.Invoke(requestProcessing, new object[] { parameterValue });
                return returnObject;
            }
            catch (Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(ex);
            }
        }
    }
}