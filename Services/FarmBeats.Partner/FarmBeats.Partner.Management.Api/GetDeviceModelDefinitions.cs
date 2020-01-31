using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using FarmBeats.Partner.Management.Api.Model;

namespace FarmBeats.Partner.Management.Api
{
    public static class GetDeviceModelDefinitions
    {
        [FunctionName("GetDeviceModelDefinitions")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext context)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = DeviceDefinition.Load();
            return new OkObjectResult(result);
        }
    }
}
