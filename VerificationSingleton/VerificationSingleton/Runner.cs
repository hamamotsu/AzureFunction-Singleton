using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VerificationSingleton
{
    public class Runner
    {
        public async Task Run(
        string instanceId,
        string myQueueItem,
        CloudTable table,
        ExecutionContext context,
        TraceWriter log)
        {
            await table.CreateIfNotExistsAsync();
            var insertOperation = TableOperation.Insert(new Result()
            {
                PartitionKey = instanceId,
                RowKey = context.InvocationId.ToString("N"),
                Category = myQueueItem,
            });
            await table.ExecuteAsync(insertOperation);
            System.Threading.Thread.Sleep(1000);
        }
    }

    public class Result : TableEntity
    {
        public string Category { get; set; }
    }
}
