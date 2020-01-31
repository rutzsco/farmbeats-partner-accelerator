using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarmBeats.Common.Model;

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
        private static Dictionary<string, Sensor> _sensorsByName;

        public IndoorM1DeviceInstanceDefinition(IEnumerable<Sensor> sensors)
        {
            _sensorsByName = sensors.GroupBy(a => a.name)
                .Select(g => g.First())
                .ToDictionary(x => x.name, x => x);
        }

        public IEnumerable<SensorTelemetry> MapToFarmBeatsTelemetryModel(string deviceName, IndoorM1Telemetry message)
        {

            var st = new SensorTelemetry();
            st.id = _sensorsByName[deviceName + IndoorM1DeviceSensorMappings.SoilMoisture1].id;
            st.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), soilmoisture = Convert.ToDouble(message.SoilMoisture1) } };

            var st2 = new SensorTelemetry();
            st2.id = _sensorsByName[deviceName + IndoorM1DeviceSensorMappings.Light].id;
            st2.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), ambientlight = Convert.ToDouble(message.Light) } };

            var st3 = new SensorTelemetry();
            st3.id = _sensorsByName[deviceName + IndoorM1DeviceSensorMappings.AirPressure].id;
            st3.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), airPressure = Convert.ToDouble(message.AirPressure) } };

            var st4 = new SensorTelemetry();
            st4.id = _sensorsByName[deviceName + IndoorM1DeviceSensorMappings.AirHumidity].id;
            st4.sensordata = new[] { new SensorData() { timestamp = DateTime.UtcNow.ToString("o"), airHumidity = Convert.ToDouble(message.AirHumidity) } };

            return new List<SensorTelemetry>() { st , st2 , st3, st4 };
        }
    }
}
