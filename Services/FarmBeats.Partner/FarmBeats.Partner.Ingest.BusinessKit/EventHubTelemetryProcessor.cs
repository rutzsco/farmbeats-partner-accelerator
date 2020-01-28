using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FarmBeats.Partner.Ingest.BusinessKit.Model;
using FarmBeats.Partner.Management.Api.Services;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FarmBeats.Partner.Ingest.BusinessKit
{
    public static class EventHubTelemetryProcessor
    {
        private static HttpClient _httpClient = new HttpClient();

        [FunctionName("EventHubTelemetryProcessor")]
        public static async Task Run([EventHubTrigger("Ingest", Connection = "EventHubInputConnectionString")] EventData[] events, 
                                     [EventHub("sensor-partner-eh-00", Connection = "EventHubOutputConnectionString")]IAsyncCollector<string> outputEvents, ILogger log, ExecutionContext executionContext)
        {
            
            // Initialize dependancies
            var config = executionContext.BuildConfiguraion();
            var token = await Extensions.GetS2SAccessToken(config);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var farmBeatsClient = new FarmBeatsClient(config["FarmBeatsApiUrl"], _httpClient);

            // Execute
            var exceptions = new List<Exception>();
            var instanceName = "EastChain - Business DevKit";
            var targetSensorConfiguration = await farmBeatsClient.GetSensors();
            var targetDeviceConfiguration = await farmBeatsClient.GetDevice(instanceName + "Indoor-M1");
            foreach (EventData eventData in events)
            {
                try
                {
                    var messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    var message = JsonConvert.DeserializeObject<IndoorM1Telemetry>(messageBody);
                    log.LogInformation($"Input Message: {messageBody} Property Keys: {string.Join(",", eventData.Properties.Keys)}  Property Values: { string.Join(",", eventData.Properties.Values)}");


                    var mapper = new IndoorM1DeviceInstanceDefinition(targetSensorConfiguration);
                    var fbTelemetry = mapper.MapToFarmBeatsTelemetryModel(instanceName, message);
                    var telemetry = new FarmBeatsTelemetryModel(targetDeviceConfiguration.id, fbTelemetry);

                    var outMessage = JsonConvert.SerializeObject(telemetry, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    log.LogInformation($"Output Message: {outMessage}");
                    await outputEvents.AddAsync(outMessage);            
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
