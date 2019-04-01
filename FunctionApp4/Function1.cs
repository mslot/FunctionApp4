using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionApp4
{
    public class Function1
    {
        private readonly ILogic logic;

        public Function1(ILogic logic)
        {
            this.logic = logic;
        }
        [FunctionName("Function1Http")]
        public async Task<IActionResult> RunHttp(
        [HttpTrigger(AuthorizationLevel.Admin, "get", "post", Route = null)]
        HttpRequest req, ILogger log)
        {
            ((Logic)logic).logger = log;
            logic.Method();
            await RunFunction(log);

            return new OkResult();
        }

        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            await RunFunction(log);
        }

        private async Task RunFunction(ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
