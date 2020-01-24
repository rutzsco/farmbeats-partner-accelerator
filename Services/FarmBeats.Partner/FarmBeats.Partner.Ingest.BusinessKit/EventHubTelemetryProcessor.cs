using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarmBeats.Partner.Ingest.BusinessKit.Model;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FarmBeats.Partner.Ingest.BusinessKit
{
    public static class EventHubTelemetryProcessor
    {
        [FunctionName("EventHubTelemetryProcessor")]
        public static async Task Run([EventHubTrigger("Ingest", Connection = "EventHubConnectionString")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    var message = JsonConvert.DeserializeObject<IndoorM1Telemetry>(messageBody);

                    // Replace these two lines with your processing logic.
                    log.LogInformation($"Processing telemetry data point: SoilMoisture1={message.SoilMoisture1}, SoilMoisture2={message.SoilMoisture2}");
                    await Task.Yield();
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
