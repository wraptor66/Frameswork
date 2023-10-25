using Azure;
using Azure.Data.Tables;
using AzureBlobTables.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ITableEntity = Azure.Data.Tables.ITableEntity;

namespace AzureBlobTables.AzureTables
{
    public class TableStorageService : ITableStorageService
    {
        private string _TableName = string.Empty;
        private string _ConnectionString = string.Empty;
        public TableStorageService(string ConnectionString, string TableName)
        {
            _ConnectionString= ConnectionString;    
            _TableName = TableName;
        }

        public async Task<TableRow> GetEntityAsync(string PartitionKey, string RowKey)
        {
            var tableClient = await GetTableClient();
            return await tableClient.GetEntityAsync<TableRow>(PartitionKey, RowKey);
        }

        public async Task<TableRow> UpsertEntityAsync(TableRow entity)
        {
            var tableClient = await GetTableClient();
            await tableClient.UpsertEntityAsync(entity);
            return entity;
        }

        public async Task DeleteEntityAsync(string category, string id)
        {
            var tableClient = await GetTableClient();
            await tableClient.DeleteEntityAsync(category, id);
        }
         /// <summary>
         /// Method creates the TableClient based on the Connection String and Table Name
         /// </summary>
         /// <returns></returns>
        private async Task<TableClient> GetTableClient()
        {
            var serviceClient = new TableServiceClient(_ConnectionString);

            var tableClient = serviceClient.GetTableClient(_TableName);
            await tableClient.CreateIfNotExistsAsync();
            return tableClient;
        }
        /// <summary>
        /// Queries the Table with the PartionKey-returns the top 1000
        /// </summary>
        /// <param name="PartitionKey"></param>
        /// <returns></returns>
        public async Task<AsyncPageable<Azure.Data.Tables.TableEntity>> QueryTableRowsWithPartitionKey(string PartitionKey)
        {
            var tableClient = await GetTableClient();
                AsyncPageable<Azure.Data.Tables.TableEntity> queryResultsFilter =
                     tableClient.QueryAsync<TableEntity>(filter: $"PartitionKey eq '{PartitionKey}'",1000);
                return queryResultsFilter;
   
        }

        /// <summary>
        /// Query the Table for all Rows within a Date Range
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public async Task<AsyncPageable<Azure.Data.Tables.TableEntity>> QueryTableRowsWithDateRange(DateTime StartDate, DateTime EndDate)
        {
            var tableClient = await GetTableClient();

            string startDate = StartDate.ToUniversalTime()
                     .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            string endDate = EndDate.ToUniversalTime()
                         .ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");

            AsyncPageable<Azure.Data.Tables.TableEntity> queryResultsFilter =
                   tableClient.QueryAsync<TableEntity>(filter: $"Timestamp ge '{startDate}' and Timestamp le '{endDate}'", 1000);
            return queryResultsFilter;
        }
    }
}

