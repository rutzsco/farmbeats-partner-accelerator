using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit.Model
{
    public class DeviceInstanceDefinition
    {
        public static IEnumerable<DeviceInstanceDefinition> All
        {
            get
            {
                yield return EastChain;
            }
        }

        public static DeviceInstanceDefinition EastChain = new DeviceInstanceDefinition("EastChain - Business DevKit", "Indoor-M1", "b827eb2f6557", "329591e0-1107-4b7a-a69c-7be83dd57aef");

        public DeviceInstanceDefinition(string name, string type, string deviceId, string farmBeatsDeviceId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            DeviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
            FarmBeatsDeviceId = farmBeatsDeviceId ?? throw new ArgumentNullException(nameof(farmBeatsDeviceId));
        }

        public string Name { get; }

        public string Type { get; }

        public string DeviceId { get; }

        public string FarmBeatsDeviceId { get; }
    }
}
