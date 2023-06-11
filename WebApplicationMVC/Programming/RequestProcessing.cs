using System.Data;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using JsonMapping.JsonFieldFinder;
using Newtonsoft.Json;

namespace Programming
{
    public class RequestProcessing
    {

        /// <summary>
        /// Example of a method that will parse the string parameter into a JObject and then
        /// use the JsonMapper.JsonFieldFinder to read the JObject for field values
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public dynamic LoginUser(string jObject)
        {
            try
            {
                JObject jobject = JObject.Parse(jObject);
                /// the line below creates every permutation of wildcards for parsing json attributes 2 levels deep
                var permutations = JsonFieldFinder.GetJPaths(2);
                /// read the json object 
                string userName = JsonFieldFinder.FindFieldValue("username", jobject, permutations).ToString();
                string password = JsonFieldFinder.FindFieldValue("password", jobject, permutations).ToString();
                KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(userName, password);
                return keyValuePair;
            }
            catch (Exception ex)
            {
                //return error message
                return ex.Message;
            }

        }

    }
}
