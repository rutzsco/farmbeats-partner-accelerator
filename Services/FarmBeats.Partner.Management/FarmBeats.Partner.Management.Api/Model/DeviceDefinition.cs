using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class DeviceDefinition
    {
        public static DeviceDefinition Load()
        {
            string result = "";
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "FarmBeats.Management.Api.DeviceModelDefinition.json";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            var deviceDefinition = JsonConvert.DeserializeObject<DeviceDefinition>(result);
            return deviceDefinition;
        }

        public IEnumerable<DeviceModel> deviceModels { get; set; }

        public IEnumerable<SensorModel> sensorModels { get; set; }
    }
}
