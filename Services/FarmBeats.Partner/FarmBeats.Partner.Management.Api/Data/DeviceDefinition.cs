using FarmBeats.Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public static class AccountConfigration
    {
        public static IEnumerable<Account> Load()
        {
            var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(LoadEmbeddedResource("FarmBeats.Partner.Management.Api.Data.Accounts.json"));
            return accounts;
        }

        private static string LoadEmbeddedResource(string resourceName)
        {
            string result = "";
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }


    public class DeviceDefinition
    {
        public DeviceDefinition(IEnumerable<DeviceModel> deviceModels, IEnumerable<SensorModel> sensorModels)
        {
            this.deviceModels = deviceModels ?? throw new ArgumentNullException(nameof(deviceModels));
            this.sensorModels = sensorModels ?? throw new ArgumentNullException(nameof(sensorModels));
        }

        public static DeviceDefinition Load()
        {
            var deviceModels = JsonConvert.DeserializeObject<IEnumerable<DeviceModel>>(LoadEmbeddedResource("FarmBeats.Partner.Management.Api.Data.DeviceModels.json"));
            var sensorModels = JsonConvert.DeserializeObject<IEnumerable<SensorModel>>(LoadEmbeddedResource("FarmBeats.Partner.Management.Api.Data.SensorModels.json"));

            return new DeviceDefinition(deviceModels, sensorModels);
        }

        public IEnumerable<DeviceModel> deviceModels { get; private set; }

        public IEnumerable<SensorModel> sensorModels { get; private set; }

        private static string LoadEmbeddedResource(string resourceName)
        {
            string result = "";
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
