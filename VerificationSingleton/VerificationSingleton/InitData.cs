using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;

namespace VerificationSingleton
{
    public static class InitData
    {
        [FunctionName("InitData")]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req,
            [Queue("nosingleton-data", Connection = "FUNC_STORAGE")]ICollector<string> testdata1,
            [Queue("singleton-data", Connection = "FUNC_STORAGE")]ICollector<string> testdata2,
            [Queue("triggersingleton-data", Connection = "FUNC_STORAGE")]ICollector<string> testdata3,
            [Queue("lowwertriggersingleton-data", Connection = "FUNC_STORAGE")]ICollector<string> testdata4,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            foreach (var item in Enumerable.Range(0, 150))
            {
                var rand = new Random();
                var category = rand.Next(4);
                testdata1.Add(category.ToString());
                testdata2.Add(category.ToString());
                testdata3.Add(category.ToString());
                testdata4.Add(category.ToString());
            }

            return req.CreateResponse(HttpStatusCode.OK, "Created data.");
        }
    }
}
