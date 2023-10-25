using Azure;
using Azure.Data.Tables;
namespace AzureBlobTables.Models
{
    public class AzureTables
	{
		public AzureTables()
		{
		}
	}

    // Define a strongly typed entity by implementing the ITableEntity interface.
    public class TableRow : ITableEntity
    {
        public string JsonObject { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
        public string PartitionKey { get; set; } = string.Empty;
        public string RowKey { get; set; } = string.Empty;
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
    public enum TableRowFields
    {
        JsonObject,
        EventName,
        ActionName,
        PartitionKey,
        RowKey
    }

}

