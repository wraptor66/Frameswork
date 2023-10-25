using Azure;
using Azure.Data.Tables;
using AzureBlobTables.AzureTables;
using AzureBlobTables.Models;

namespace AzureBlobTables
{
    public class BlobTable
    {
        public enum ConfigArgType
        {
            TableName,
            ConnectionString
        }
        private string _TableName = string.Empty; 
        private string _ConnectionString = string.Empty;


        public enum TableRowArgType
        {
            JsonObject,
            EventName,
            ActionName,
            PartitionKey,
            RowKey
        }
        public BlobTable(List<KeyValuePair<ConfigArgType, string>> Args)
        {
            _ConnectionString = Args.FirstOrDefault(x=>x.Key == ConfigArgType.ConnectionString).Value;
            _TableName = Args.FirstOrDefault(x => x.Key == ConfigArgType.TableName).Value;
        }

        public async Task InsertLog(TableRow TableRow)
        {
            if (TableRow != null)
            {
                TableStorageService tableStorageService = new TableStorageService(_ConnectionString,_TableName);
                await tableStorageService.UpsertEntityAsync(TableRow);
            }
        }

        public async Task DeleteLog(string PartitionKey, string RowKey)
        {
            if (!String.IsNullOrEmpty(PartitionKey) && !String.IsNullOrEmpty(RowKey))
            {
                TableStorageService tableStorageService = new TableStorageService(_ConnectionString, _TableName);
                await tableStorageService.DeleteEntityAsync(PartitionKey, RowKey);
            }
        }

        public async Task<TableRow> GetRow(List<KeyValuePair<TableRowArgType, string>> Args)
        {
            TableStorageService tableStorageService = new TableStorageService(_ConnectionString, _TableName);

            string PartitionKey = Args.FirstOrDefault(x => x.Key == TableRowArgType.PartitionKey).Value;
            string RowKey = Args.FirstOrDefault(x => x.Key == TableRowArgType.RowKey).Value;

           return await tableStorageService.GetEntityAsync(PartitionKey, RowKey);
        }

        public async Task<AsyncPageable<Azure.Data.Tables.TableEntity>> GetRowsWithPartitionKey(string PartitionKey)
        {
            TableStorageService tableStorageService = new TableStorageService(_ConnectionString, _TableName);

            return await tableStorageService.QueryTableRowsWithPartitionKey(PartitionKey);
        }

        public async Task<AsyncPageable<Azure.Data.Tables.TableEntity>> QueryTableRowsWihDateRange(DateTime StartDate, DateTime EndDate)
        {
            TableStorageService tableStorageService = new TableStorageService(_ConnectionString, _TableName);

            return await tableStorageService.QueryTableRowsWithDateRange(StartDate,EndDate);
        }

    }
}