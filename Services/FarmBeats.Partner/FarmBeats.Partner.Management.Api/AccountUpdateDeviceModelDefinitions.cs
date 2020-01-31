using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FarmBeats.Partner.Management.Api.Model;
using System.Net.Http;
using FarmBeats.Common;
using System.Net.Http.Headers;
using System.Linq;

namespace FarmBeats.Partner.Management.Api
{
    public static class AccountUpdateDeviceModelDefinitions
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("AccountUpdateDeviceModelDefinitions")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var config = executionContext.BuildConfiguraion();
            var account = AccountConfigration.Load().First();
            var deviceDefinition = DeviceDefinition.Load();

            var token = await Extensions.GetS2SAccessToken(account.ClientId, config[account.ClientSecretSettingName], account.Resource, account.Authority);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var fb = new FarmBeatsClient(account.Url, _httpClient);

            foreach (var dm in deviceDefinition.deviceModels)
            {
                await fb.CreateDeviceModel(dm);
            }

            foreach (var sm in deviceDefinition.sensorModels)
            {
                await fb.CreateSensorModel(sm);
            }

            return new OkObjectResult("OK");
        }
    }
}
