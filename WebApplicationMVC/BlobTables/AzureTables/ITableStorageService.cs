using System;
using System.Threading.Tasks;
using Azure;
using AzureBlobTables.Models;
namespace AzureBlobTables.AzureTables
{
	public interface ITableStorageService
	{

        Task<TableRow> GetEntityAsync(string category, string id);
        Task<TableRow> UpsertEntityAsync(TableRow entity);
        Task DeleteEntityAsync(string category, string id);
        Task<AsyncPageable<Azure.Data.Tables.TableEntity>> QueryTableRowsWithPartitionKey(string PartitionKey);
        Task<AsyncPageable<Azure.Data.Tables.TableEntity>> QueryTableRowsWithDateRange(DateTime StartDate, DateTime EndDate);
    }
}

