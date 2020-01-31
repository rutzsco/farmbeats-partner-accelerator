using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FarmBeats.Common.Model;
using System.Net.Http;
using FarmBeats.Common;
using System.Net.Http.Headers;
using System.Linq;
using FarmBeats.Partner.Management.Api.Model;

namespace FarmBeats.Partner.Management.Api
{
    public static class AccountUpdateDeviceInstances
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("AccountUpdateDeviceInstances")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req, ILogger log, ExecutionContext executionContext)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var config = executionContext.BuildConfiguraion();
            var account  = AccountConfigration.Load().First();

            var token = await Extensions.GetS2SAccessToken(account.ClientId, config[account.ClientSecretSettingName], account.Resource, account.Authority);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var fb = new FarmBeatsClient(account.Url, _httpClient);
            foreach (var farm in account.farms)
            {
                foreach (var device in farm.devices)
                {
                    var deviceModel = fb.GetDeviceModel(device.deviceModel).Result;
                    var deviceDetail = await fb.CreateDevice(new Device(Guid.NewGuid().ToString(), deviceModel.id, farm.id, new Location(0, 0), device.name + deviceModel.name));

                    foreach (var sensor in device.sensors)
                    {
                        var sensorModel = fb.GetSensorModel(sensor).Result;
                        await fb.CreateSensor(new Sensor(Guid.NewGuid().ToString(), sensorModel.id, deviceDetail.id, device.name + sensorModel.name));
                    }
                }                 
            }

            return new OkObjectResult("OK");
        }
    }
}
