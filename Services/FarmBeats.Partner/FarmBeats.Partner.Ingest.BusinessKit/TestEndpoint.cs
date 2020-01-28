using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.EventHubs;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit
{
    public static class TestEndpoint
    {
        [FunctionName("TestEndpoint")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var config = executionContext.BuildConfiguraion();

            var message = await new StreamReader(req.Body).ReadToEndAsync();
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(config["EventHubOutputConnectionString"]);
            var eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));

            return new OkObjectResult("OK");
        }
    }
}
