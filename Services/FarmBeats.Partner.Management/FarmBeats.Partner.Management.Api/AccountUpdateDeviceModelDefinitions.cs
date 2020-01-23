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
using FarmBeats.Partner.Management.Api.Services;
using System.Net.Http.Headers;

namespace FarmBeats.Partner.Management.Api
{
    public static class AccountUpdateDeviceModelDefinitions
    {
        private static HttpClient _httpClient = new HttpClient();

        [FunctionName("AccountUpdateDeviceModelDefinitions")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
 
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var request = JsonConvert.DeserializeObject<AccountCredentials>(requestBody);

            var token = await Extensions.GetS2SAccessToken(request.ClientId, request.ClientSecret, request.Resource, request.Authority);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var deviceDefinition = DeviceDefinition.Load();

            var fb = new FarmBeatsClient(request.Url, _httpClient);

            foreach (var dm in deviceDefinition.deviceModels)
            {
                await fb.CreateDeviceModel(dm);
            }

            foreach (var sm in deviceDefinition.sensorModels)
            {
                await fb.CreateSensorModel(sm);
            }

            return new OkObjectResult("");
        }
    }
}
