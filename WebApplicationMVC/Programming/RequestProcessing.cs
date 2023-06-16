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

        /// <summary>
        /// Example of a method that will parse the string parameter into a JObject and then
        /// use the JsonMapper.JsonFieldFinder to read the JObject for field values
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public dynamic EnrichData(string jObject)
        {
            try
            {
                JObject jobject = JObject.Parse(jObject);
                /// the line below creates every permutation of wildcards for parsing json attributes 2 levels deep
                var permutations = JsonFieldFinder.GetJPaths(2);
                /// read the json object 
                string userName = JsonFieldFinder.FindFieldValue("inputfield1", jobject, permutations).ToString();
                string password = JsonFieldFinder.FindFieldValue("inputfield2", jobject, permutations).ToString();
                userName = $"{userName}::{DateTime.Now.ToString()}";
                password = $"{password}::{DateTime.Now.ToString()}";
                KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(userName, password);
                return keyValuePair;
            }
            catch (Exception ex)
            {
                //return error message
                return ex.Message;
            }

        }

        /// <summary>
        /// Example of a method that will parse the string parameter into a JObject and then
        /// use the JsonMapper.JsonFieldFinder to read the JObject for field values
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public dynamic GetDataSet(string jObject)
        {
            try
            {
                /// here is a sample json object with the a 'nested' collection
                /// json can be acquired from web api's, or sql server queries (add-> for json auto)
                string stringJson = "{'store':{'book':[{'category':'reference','author':'Nigel Rees','title':'Sayings of the Century','price':8.95},{'category':'fiction','author':'Evelyn Waugh','title':'Sword of Honour','price':12.99},{'category':'fiction','author':'J. R. R. Tolkien','title':'The Lord of the Rings','isbn':'0-395-19395-8','price':22.99}],'bicycle':{'color':'red','price':19.95}}}";
                JObject keyValuePairs = JObject.Parse(stringJson);

                object books = JsonFieldFinder.FindFieldValue("store.book", keyValuePairs,JsonFieldFinder.GetJPaths(5));
                return books; 
            }
            catch (Exception ex)
            {
                //return error message
                return ex.Message;
            }

        }
    }
}
