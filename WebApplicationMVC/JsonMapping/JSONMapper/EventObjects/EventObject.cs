using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace JsonMapping.EventObjects
{
    public class EventObject
    {
        public enum MapName
        {
            eventmap
        }
        public List<KeyValuePair<string, string>> _jsonCollection { get; set; }
        public string ParentObject = string.Empty;
        public List<KeyValuePair<string, string>> dataPayloadKeywords { get; set; }
        public EventObject()
        {

        }
        public object GetDateTime()
        {
            return DateTime.UtcNow;
        }

        public List<KeyValuePair<string, string>> GetJsonCollection()
        {
            return _jsonCollection;
        }
        public object GetJsonObject(string StringValue)
        {
            return JObject.Parse("{ \"jsonobject\" : \"" + StringValue + "\"}");
        }

        public string ProcessJsonObject(string jObject)
        {
            /// call the crud
            /// eventtrigger is the field calling this method
            /// needs to be removed from the jschema before submitting

            JToken jobject = JToken.Parse(jObject);
            foreach (var ea in jobject.Children())
            {
                if (((JProperty)ea).Name == "eventtrigger")
                {
                    ea.Remove();
                    Console.WriteLine("call the crud: " + jobject);
                    break;

                }
            }

            return null;
        }
    }
}
