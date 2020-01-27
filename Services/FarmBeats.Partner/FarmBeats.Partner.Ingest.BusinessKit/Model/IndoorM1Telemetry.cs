using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit.Model
{
    public class IndoorM1Telemetry
    {
        public double SoilMoisture1 { get; set; }

        public double SoilMoisture2 { get; set; }

        public double AmbientLight { get; set; }

        public double Humidity { get; set; }
    }
}
