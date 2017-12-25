using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;

namespace VerificationSingleton
{
    public static class SingletonTest
    {
        static Lazy<Guid> lazyInstanceId = new Lazy<Guid>(() => Guid.NewGuid());

        [Singleton("SingletonTest")]
        [FunctionName("SingletonTest")]
        public static async Task RunSingleton(
            [QueueTrigger("singleton-data", Connection = "FUNC_STORAGE")]string myQueueItem,
            [Table("singletonresult", Connection = "FUNC_STORAGE")] CloudTable save,
            ExecutionContext context,
            TraceWriter log)
        {
            var runner = new Runner();
            await runner.Run(lazyInstanceId.Value.ToString("N"), myQueueItem, save, context, log);
        }
    }


}
