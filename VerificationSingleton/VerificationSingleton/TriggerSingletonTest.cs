using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace VerificationSingleton
{
    public static class TriggerSingletonTest
    {
        static Lazy<Guid> lazyInstanceId = new Lazy<Guid>(() => Guid.NewGuid());

        [Singleton("{QueueTrigger}")]
        [FunctionName("TriggerSingletonTest")]
        public static async Task RunTriggerSingleton(
            [QueueTrigger("triggersingleton-data", Connection = "FUNC_STORAGE")]string myQueueItem,
            [Table("triggersingletonresult", Connection = "FUNC_STORAGE")] CloudTable save,
            ExecutionContext context,
            TraceWriter log)
        {
            var runner = new Runner();
            await runner.Run(lazyInstanceId.Value.ToString("N"), myQueueItem, save, context, log);
        }
    }
}
