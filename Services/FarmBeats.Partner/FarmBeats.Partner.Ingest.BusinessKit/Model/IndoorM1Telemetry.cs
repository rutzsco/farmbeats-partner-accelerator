using FarmBeats.Partner.Management.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public static class IndoorM1DeviceSensorMappings
    {
        public static string SoilMoisture1 = "Grove Soil Moisture";
        public static string Light = "Grove Ambient Light";
        public static string AirPressure = "Grove Air Pressure";
        public static string AirHumidity = "Grove Air Humidity";
    }

    public class IndoorM1DeviceInstanceDefinition
    {
        private static Dictionary<string, SensorModel> _sensorsByName;

        public IndoorM1DeviceInstanceDefinition(IEnumerable<SensorModel> sensors)
        {
            _sensorsByName = sensors.ToDictionary(x => x.productCode, x => x);
        }

        public IEnumerable<SensorTelemetry> MapToFarmBeatsTelemetryModel(IndoorM1Telemetry message)
        {

            var st = new SensorTelemetry();
            //st.id = "8096581b-90d8-447d-924b-4ef16b6fd40d";
            st.id = _sensorsByName[IndoorM1DeviceSensorMappings.SoilMoisture1].id;
            st.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), soilmoisture = Convert.ToDouble(message.SoilMoisture1) } };

            var st2 = new SensorTelemetry();
            //st2.id = "a7af7283-9cd7-4c26-ab5c-3ecfe9d58acf";
            st2.id = _sensorsByName[IndoorM1DeviceSensorMappings.Light].id;
            st2.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), ambientlight = Convert.ToDouble(message.Light) } };

            var st3 = new SensorTelemetry();
            //st3.id = "4c300af4-906d-42e0-aace-de59d8db694b";
            st3.id = _sensorsByName[IndoorM1DeviceSensorMappings.AirPressure].id;
            st3.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), airPressure = Convert.ToDouble(message.AirPressure) } };

            var st4 = new SensorTelemetry();
            //st4.id = "7c88953b-a4a8-4dc3-9d93-85a96f445ae6";
            st4.id = _sensorsByName[IndoorM1DeviceSensorMappings.AirHumidity].id;
            st4.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), airHumidity = Convert.ToDouble(message.AirHumidity) } };

            return new List<SensorTelemetry>() { st , st2 , st3, st4 };
        }
    }
}
