using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit.Model
{
    public class FarmBeatsTelemetryModel
    {
        public FarmBeatsTelemetryModel(string deviceid, IEnumerable<SensorTelemetry> sensors)
        {
            this.deviceid = deviceid;
            this.sensors = sensors;
            this.version = "1";
            this.timestamp = DateTime.UtcNow.ToString("o");
        }

        public string deviceid { get; set; }

        public string farmId { get; set; }

        public string version { get; set; }
        public string timestamp { get; set; }

        public IEnumerable<SensorTelemetry> sensors { get; set; }
    }

    public class SensorTelemetry
    {
        public string id { get; set; }

        public IEnumerable<SensorData> sensordata { get; set; }
    }

    public class SensorData
    {
        public string timestamp { get; set; }

        public double? temperature { get; set; }

        public double? level { get; set; }

        public double? soilmoisture { get; set; }

        public double? ambientlight { get; set; }

        public double? airPressure { get; set; }

        public double? airHumidity { get; set; }

        public double? reading1 { get; set; }

        public double? reading2 { get; set; }
    }
}
