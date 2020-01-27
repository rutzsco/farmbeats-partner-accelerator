using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit.Model
{
    //{"AirTemperatureC":20.25,"AirTemperatureF":68.449999999999989,"AirHumidity":43.611328125,"AirPressure":98.10033,"Light":24.7,"SoilMoisture1":59.3,"SoilMoisture2":53.2}
    public class IndoorM1Telemetry
    {
        public double SoilMoisture1 { get; set; }

        public double SoilMoisture2 { get; set; }

        public double Light { get; set; }

        public double AirPressure { get; set; }

        public double AirHumidity { get; set; }

        public double AirTemperatureF { get; set; }

        public double AirTemperatureC { get; set; }
    }
}
