using System.Data;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using JsonMapping.JsonFieldFinder;
using Newtonsoft.Json;
using Azure.Data.Tables;
using AzureBlobTables.AzureTables;
using AzureBlobTables;

namespace Programming
{
    public class RequestProcessing
    {

        readonly string BlobConnString = string.Empty;
        public RequestProcessing()
        {

            var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json").Build();
            BlobConnString = configuration.GetValue<string>("BlobTableConnString");

        }

            /// <summary>
            /// Example of a method that will parse the string parameter into a JObject and then
            /// use the JsonMapper.JsonFieldFinder to read the JObject for field values
            /// </summary>
            /// <param name="jObject"></param>
            /// <returns></returns>
            public dynamic ParseUserCredentials(string jObject)
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

        /// <summary>
        /// Register User Orchestration
        /// </summary>
        /// <param name="jObjectAsString"></param>
        /// <returns></returns>
        public dynamic RegisterUser(string jObjectAsString)
        {
            ///
            /// create a blob take client
            /// 
            List<KeyValuePair<AzureBlobTables.BlobTable.ConfigArgType, string>> keyValuePairs = new List<KeyValuePair<BlobTable.ConfigArgType, string>>();
            keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                (BlobTable.ConfigArgType.TableName, "logs"));
            keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                (BlobTable.ConfigArgType.ConnectionString, BlobConnString));
            AzureBlobTables.BlobTable blobTable = new BlobTable(keyValuePairs);

            JObject jObject = JObject.Parse(jObjectAsString);

            InsertTableRow(jObjectAsString, blobTable, RowType.Information);

            string email = Convert.ToString(jObject["userName"]);
            string password = PasswordManager.HashPassword(Convert.ToString(jObject["password"]));
            jObject["password"] = password;

            ///Check for the existence of the email address as a partition key
            if (Convert.ToBoolean(GetTableRow(email, blobTable, ReturnRowFields.RowExist).Result))
            {
                ///the email is already in the repository
                return new KeyValuePair<string, string>("Error", "Email Already Exists");
            }

            ///insert the registration row for the email address
            InsertTableRow(jObject.ToString(), blobTable, RowType.Registration);

            ///Checawaitk for the existence of the email address as a partition key
            if (GetTableRow(email, blobTable, ReturnRowFields.RowExist).Result)
            {
                ///everything is processed fine
                ///send an email with information

                EmailObject emailObject = new EmailObject();
                emailObject.email = Convert.ToString(jObject["userName"]);
                emailObject.title = "Registration for VidTalk";
                emailObject.message = $"Your UserName is {Convert.ToString(jObject["userName"])}";
                var result = new Communications.SendGridAPI().SendEmail(JsonConvert.SerializeObject(emailObject));
                return new KeyValuePair<string, string>("Success", GetTableRow(email, blobTable, ReturnRowFields.RowID).Result);
            }
            else
            {
                ///the email is already in the repository
                return new KeyValuePair<string, string>("Error", $"Registration Incomplete {Convert.ToString(jObject["CorrelationID"])}");

            }
        }

        /// <summary>
        /// Delete Vidtalk User Orchestration
        /// </summary>
        /// <param name="jObjectAsString"></param>
        /// <returns></returns>
        public dynamic DeleteUser(string jObjectAsString)
        {
            try
            {
                ///
                /// create a blob take client
                /// 
                List<KeyValuePair<AzureBlobTables.BlobTable.ConfigArgType, string>> keyValuePairs = new List<KeyValuePair<BlobTable.ConfigArgType, string>>();
                keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                    (BlobTable.ConfigArgType.TableName, "logs"));
                keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                    (BlobTable.ConfigArgType.ConnectionString, BlobConnString));
                AzureBlobTables.BlobTable blobTable = new BlobTable(keyValuePairs);

                JObject jObject = JObject.Parse(jObjectAsString);


                string email = Convert.ToString(jObject["userName"]);
                string password = Convert.ToString(jObject["password"]);

                if (Convert.ToBoolean(GetTableRow(email, blobTable, ReturnRowFields.RowExist).Result))
                {
                    ///the email is already in the repository
                    ///get the complete row
                    List<KeyValuePair<BlobTable.TableRowArgType, string>> keyValues =
                        new List<KeyValuePair<BlobTable.TableRowArgType, string>>();
                    keyValues.Add(new KeyValuePair<BlobTable.TableRowArgType, string>(
                        BlobTable.TableRowArgType.PartitionKey, email));
                    string rowKey = GetUserLoginRowKey(email, password, blobTable).Result;
                    keyValues.Add(new KeyValuePair<BlobTable.TableRowArgType, string>(
                        BlobTable.TableRowArgType.RowKey, rowKey));
                    AzureBlobTables.Models.TableRow tableentity = blobTable.GetRow(keyValues).Result;

                    ///delete the row
                    AzureBlobTables.AzureTables.TableStorageService tableStorageService =
                           new TableStorageService(BlobConnString, "logs");

                    _ = tableStorageService.DeleteEntityAsync(tableentity.PartitionKey, tableentity.RowKey);

                    ///insert a log using the azure blob table.
                    AzureBlobTables.Models.TableRow TableRow = new AzureBlobTables.Models.TableRow();
                    TableRow.PartitionKey = $"deleted|{tableentity.PartitionKey}";
                    TableRow.ActionName = $"deleted|{tableentity.ActionName}";
                    TableRow.EventName = $"deleted|{tableentity.EventName}";
                    TableRow.RowKey = $"deleted|{tableentity.RowKey}";
                    TableRow.JsonObject = $"deleted|{JsonConvert.SerializeObject(tableentity)}";
                    _ = blobTable.InsertLog(TableRow);

                    return new KeyValuePair<string, string>("Success",
                $"Your account has now been deleted. You will need to create a new account to use the service.");
                }
                return new KeyValuePair<string, string>("Failure",
                $"The login credentials are not found. Please confirm your email for your encoded password.");



            }
            catch (Exception ex)
            {
                return new KeyValuePair<string, string>("Error", ex.Message);

            }
        }
        private enum RowType
        {
            Registration,
            Error,
            Deletion,
            Log,
            Information

        }
        private enum ReturnRowFields
        {
            RowExist,
            RowID,
            JsonObject,
            TimeStamp,
            Row
        }
        private async Task<dynamic> GetTableRow(string PartitionKey, AzureBlobTables.BlobTable blobTable,
            ReturnRowFields rowFields)
        {
            Azure.AsyncPageable<TableEntity> result = await blobTable.GetRowsWithPartitionKey(PartitionKey);

            await foreach (var ea in result)
            {
                switch (rowFields)
                {
                    case ReturnRowFields.RowExist:
                        {
                            return true;
                        }
                    case ReturnRowFields.RowID:
                        {
                            return ea.RowKey;
                        }
                    case ReturnRowFields.Row:
                        {
                            return ea;
                        }
                }
                return true;
            }
            return false;
        }        
        private async Task<List<TableEntity>> GetTableRows(string PartitionKey, AzureBlobTables.BlobTable blobTable)
        {
            Azure.AsyncPageable<TableEntity> result = await blobTable.GetRowsWithPartitionKey(PartitionKey);
            List<TableEntity> tableEntities = new List<TableEntity>();
            ///filter for the password in the action
            await foreach (var ea in result)
            {
                tableEntities.Add(ea);
            }
            return tableEntities;
        }
        /// <summary>
        /// This mothod is designed to process all TableRowTypes
        /// Adding a new Table Row Type 
        /// 1. Add a new type to RowType
        /// 2. Add a new 'case' condition for the above row type
        /// 3. Add code to populate the table fields, using any declare before the switch.
        /// </summary>
        /// <param name="jObjectAsString">json object, as string, parse any field</param>
        /// <param name="blobTable">blob table client</param>
        /// <param name="rowType">row type for parsing structure</param>
        /// <returns></returns>
        private void InsertTableRow(string jObjectAsString, AzureBlobTables.BlobTable blobTable,
            RowType rowType)
        {
            JObject jObject = JObject.Parse(jObjectAsString);
            ///these variables are available for all switch/cases
            string CorrelationId = jObject["correlationId"].ToString();
            var methodBase = MethodBase.GetCurrentMethod();
            string classname = methodBase.DeclaringType.Name;
            string methodname = methodBase.Name;
            string rowKey = Guid.NewGuid().ToString();
            ///insert a log using the azure blob table.
            AzureBlobTables.Models.TableRow TableRow = new AzureBlobTables.Models.TableRow();
            TableRow.RowKey = rowKey;
            switch (rowType)
            {
                case RowType.Registration:
                    {
                        string email = Convert.ToString(jObject["userName"]);
                        string password = Convert.ToString(jObject["password"]);
                        string eventName = Convert.ToString(jObject["eventname"]);
                        TableRow.JsonObject = jObjectAsString;
                        TableRow.EventName = "registration";
                        TableRow.ActionName = password;
                        TableRow.PartitionKey = email;
                        break;
                    }
                case RowType.Error:
                    {
                        TableRow.JsonObject = jObjectAsString;
                        TableRow.EventName = "error";
                        TableRow.ActionName = Convert.ToString(jObject["errormessage"]);
                        TableRow.PartitionKey = CorrelationId;
                        break;
                    }
                case RowType.Information:
                    {
                        TableRow.JsonObject = jObjectAsString;
                        TableRow.EventName = classname + "." + methodname;
                        TableRow.ActionName = "information";
                        TableRow.PartitionKey = CorrelationId;
                        break;
                    }
                default:
                    {
                        TableRow.JsonObject = jObjectAsString;
                        TableRow.EventName = classname + "." + methodname;
                        TableRow.ActionName = "log";
                        TableRow.PartitionKey = CorrelationId;
                        break;
                    }
            }

            blobTable.InsertLog(TableRow);
        }
        /// <summary>
        /// Register User Orchestration
        /// </summary>
        /// <param name="jObjectAsString"></param>
        /// <returns></returns>
        public dynamic LoginUser(string jObjectAsString)
        {
            try
            {
                ///
                /// create a blob table client
                /// 
                List<KeyValuePair<AzureBlobTables.BlobTable.ConfigArgType, string>> keyValuePairs = new List<KeyValuePair<BlobTable.ConfigArgType, string>>();
                keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                    (BlobTable.ConfigArgType.TableName, "logs"));
                keyValuePairs.Add(new KeyValuePair<BlobTable.ConfigArgType, string>
                    (BlobTable.ConfigArgType.ConnectionString, BlobConnString));
                AzureBlobTables.BlobTable blobTable = new BlobTable(keyValuePairs);
                InsertTableRow(jObjectAsString, blobTable, RowType.Information);
                JObject jObject = JObject.Parse(jObjectAsString);
                string email = Convert.ToString(jObject["userName"]);
                string password = Convert.ToString(jObject["password"]);
                var tableEntity = ConfirmUserLogin(email, password, blobTable).Result;
                if (PasswordManager.VerifyPassword(password, tableEntity["ActionName"].ToString()))
                {
                    KeyValuePair<string, string> keyValuePair =
                        new KeyValuePair<string, string>("Success", "User Credentials Exist");
                    return keyValuePair;
                }
                else
                {
                    KeyValuePair<string, string> keyValuePair =
                        new KeyValuePair<string, string>("Failure", "User Credentials Don't Exist");
                    return keyValuePair;
                }
            }
            catch (Exception ex)
            {
                KeyValuePair<string, string> keyValuePair =
                        new KeyValuePair<string, string>("Error", $"{ex.Message}");
                return keyValuePair;
            }
        }

        private async Task<TableEntity> ConfirmUserLogin(string PartitionKey, string Password,
                AzureBlobTables.BlobTable blobTable)
        {
            Azure.AsyncPageable<TableEntity> result = await blobTable.GetRowsWithPartitionKey(PartitionKey);
            List<TableEntity> tableEntities = new List<TableEntity>();

            ///filter for the password in the action
            await foreach (var ea in result)
            {
                if (ea["EventName"].ToString() == "registration")
                {
                    return ea;
                }
            }
            return null;
        }
         private async Task<string> GetUserLoginRowKey(string PartitionKey, string Password,
        AzureBlobTables.BlobTable blobTable)
        {
            Azure.AsyncPageable<TableEntity> result = await blobTable.GetRowsWithPartitionKey(PartitionKey);
            List<TableEntity> tableEntities = new List<TableEntity>();
            ///filter for the password in the action
            await foreach (var ea in result)
            {
                if (ea["ActionName"].ToString() == Password)
                {
                    return ea.RowKey;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Model for emailing
        /// </summary>
        public class EmailObject
        {
            public string email { get; set; }
            public string title { get; set; }
            public string message { get; set; }
        }
    }
}
