using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace VerificationSingleton
{
    public static class LowwerTrrigetSingletonTest
    {
        static Lazy<Guid> lazyInstanceId = new Lazy<Guid>(() => Guid.NewGuid());

        [Singleton("{queueTrigger}")]
        [FunctionName("LowwerTrrigetSingletonTest")]
        public static async Task RunTriggerSingleton(
            [QueueTrigger("lowwertriggersingleton-data", Connection = "FUNC_STORAGE")]string myQueueItem,
            [Table("lowwertriggersingletonresult", Connection = "FUNC_STORAGE")] CloudTable save,
            ExecutionContext context,
            TraceWriter log)
        {
            var runner = new Runner();
            await runner.Run(lazyInstanceId.Value.ToString("N"), myQueueItem, save, context, log);
        }
    }
}
