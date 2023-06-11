using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using JsonMapping.EventObjects;

namespace JsonMapping
{
    /// <summary>
    /// This Class should provide to wrapper of the JSONMapperfunctionality. It should
    /// also provide interface for instructions as to what type of mapping that should be 
    /// performed.
    /// </summary>
    public class MappingManager
    {
        public string ExecuteDataMapping(EventObject eventObject)
        {
            Console.WriteLine("Starting ExecuteDataMapping()");
            
            var jsonCollection = eventObject.GetJsonCollection();

            JObject joEventMap = JObject.Parse(jsonCollection
                .Find(x => x.Key == "eventmap").Value.ToString());
            string primaryobject = ((JValue)joEventMap
                .SelectToken("event.primaryobject")).Value.ToString();
            /// <summary>
            /// DataPayload Types
            /// anything exclusive of the list is an actual payload for parsing
            /// </summary>
            /// 
            List<KeyValuePair<string, string>> dataPayloadKeywords = new
            List<KeyValuePair<string, string>>();
            dataPayloadKeywords.Add(new KeyValuePair<string, string>("JObject", "Map2RequestedChild"));
            dataPayloadKeywords.Add(new KeyValuePair<string, string>("EObject", "CustomMethod"));
            dataPayloadKeywords.Add(new KeyValuePair<string, string>("JArray", "Map2RequestedChildren"));
            string returnobject = string.Empty;
            eventObject.ParentObject = primaryobject;
            eventObject.dataPayloadKeywords = dataPayloadKeywords;
            returnobject = JsonMapping.JSONMapper.JSONMapper
                .Map2Requested(eventObject);
            return returnobject;
        }

        public enum MappingAction
        {
            ValidatePayload, /// does payload has values for all the required fields
            PopulatePayload, /// traditional query data payloads and assign
            UpdatePayload, /// update exact payload with new values
            PopulateMasterObject /// assign data payoad values to Master Object
        }

        private class ServiceInfo
        {
            public string servicename { get; set; } //name for referencing within the jsoncollection
            public string serviceroute { get; set; }//the route is the equivalent of method call
            public string parentobject { get; set; }//json string for arguments (must be serialized)
            public string serviceargs { get; set; }//json object for deserializing to crud requirements
            public string servicetype { get; set; }//since the calls to crud will be methods this may be deprecated
        }
    }
}
