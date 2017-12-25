using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace VerificationSingleton
{
    public static class QueueSIngleton
    {
        [FunctionName("QueueSIngleton")]
        public static void Run(
            [QueueTrigger("singleton-data", Connection = "FUNC_STORAGE")]string myQueueItem,
            [Table("MemoryDevelopment", Connection = "FUNC_STORAGE")] ICollector<PurchaseOrder> save,
            TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
