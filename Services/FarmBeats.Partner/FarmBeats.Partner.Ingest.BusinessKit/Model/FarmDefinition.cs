using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Ingest.BusinessKit.Model
{
    public class FarmDefinition
    {
        public static FarmDefinition EastChain = new FarmDefinition("EastChain", new DeviceInstanceDefinition("", "329591e0-1107-4b7a-a69c-7be83dd57aef"));

        public FarmDefinition(string name, DeviceInstanceDefinition deviceInstanceDefinition)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DeviceInstanceDefinition = deviceInstanceDefinition ?? throw new ArgumentNullException(nameof(deviceInstanceDefinition));
        }

        public string Name { get;}

        public DeviceInstanceDefinition DeviceInstanceDefinition { get; }
    }

    public class DeviceInstanceDefinition
    {
        public DeviceInstanceDefinition(string @internal, string externalId)
        {
            Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
            ExternalId = externalId ?? throw new ArgumentNullException(nameof(externalId));
        }

        public string Internal { get; }

        public string ExternalId { get; }
    }
}
