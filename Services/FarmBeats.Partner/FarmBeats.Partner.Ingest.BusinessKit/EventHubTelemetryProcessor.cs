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
        public static async Task Run([EventHubTrigger("Ingest", Connection = "EventHubInputConnectionString")] EventData[] events, 
                                     [EventHub("sensor-partner-eh-00", Connection = "EventHubOutputConnectionString")]IAsyncCollector<FarmBeatsTelemetryModel> outputEvents, ILogger log)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    var messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);
                    var message = JsonConvert.DeserializeObject<IndoorM1Telemetry>(messageBody);
                    log.LogInformation($"Input Message: {messageBody}");

                    // Replace these two lines with your processing logic.
                    //log.LogInformation($"Processing telemetry data point: SoilMoisture1={message.SoilMoisture1}, SoilMoisture2={message.SoilMoisture2}");
                    
                    var st = new SensorTelemetry();
                    st.id = "8096581b-90d8-447d-924b-4ef16b6fd40d";
                    st.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), soilmoisture = Convert.ToDouble(message.SoilMoisture1) } };

                    var st2 = new SensorTelemetry();
                    st2.id = "a7af7283-9cd7-4c26-ab5c-3ecfe9d58acf";
                    st2.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), ambientlight = Convert.ToDouble(message.Light) } };

                    var st3 = new SensorTelemetry();
                    st3.id = "4c300af4-906d-42e0-aace-de59d8db694b";
                    st3.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), airPressure = Convert.ToDouble(message.AirPressure) } };

                    var telemetry = new FarmBeatsTelemetryModel("99800e4b-dc28-4ea8-b742-6a7a71861a8e", new[] { st, st2, st3 });
                    var outMessage = JsonConvert.SerializeObject(telemetry, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    log.LogInformation($"Output Message: {outMessage}");
                    await outputEvents.AddAsync(telemetry);            
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
