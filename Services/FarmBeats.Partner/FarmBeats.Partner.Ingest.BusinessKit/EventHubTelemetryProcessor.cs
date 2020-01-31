using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FarmBeats.Common;
using FarmBeats.Partner.Ingest.BusinessKit.Model;
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
                                     [EventHub("sensor-partner-eh-00", Connection = "EventHubOutputConnectionString")]IAsyncCollector<string> outputEvents, ILogger logger, ExecutionContext executionContext)
        {
            
            // Initialize dependancies
            var config = executionContext.BuildConfiguraion();

            var token = await Extensions.GetS2SAccessToken(config);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            var farmBeatsClient = new FarmBeatsClient(config["FarmBeatsApiUrl"], _httpClient);

            // Execute
            var exceptions = new List<Exception>();
            var targetSensorConfiguration = await farmBeatsClient.GetSensors();
            
            foreach (EventData eventData in events)
            {
                try
                {
                    // Parse message
                    var messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    logger.LogInformation($"Input Message: {messageBody} Property Keys: {string.Join(",", eventData.SystemProperties.Keys)}  Property Values: { string.Join(",", eventData.SystemProperties.Values)}");

                    var message = JsonConvert.DeserializeObject<IndoorM1Telemetry>(messageBody);                 
                    var deviceId = (string)eventData.SystemProperties["iothub-connection-device-id"];
                    var deviceDefinition = await farmBeatsClient.GetDeviceByHardwareId(deviceId);

                    // Contextualize - resolve device type and associated farmbeats device instance
                    if (deviceDefinition != null)
                    {

                        // Convert to FarmBeats Telemetry Message
                        var mapper = new IndoorM1DeviceInstanceDefinition(targetSensorConfiguration);
                        var fbTelemetry = mapper.MapToFarmBeatsTelemetryModel(deviceDefinition.name, message);
                        var telemetry = new FarmBeatsTelemetryModel(deviceDefinition.id, fbTelemetry);

                        // Send to FarmBeats EventHub
                        var outMessage = JsonConvert.SerializeObject(telemetry, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        logger.LogInformation($"Output Message: {outMessage}");
                        await outputEvents.AddAsync(outMessage);
                    }
                    else
                    {
                        logger.LogWarning($"Unsuppored device - Id: {deviceId}");
                    }
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
