using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class AccountConfigurationSummary
    {
        public string name { get; set; }

        public IEnumerable<FarmSummary> farms { get; set; }
    }

    public class FarmSummary
    {
        public string id { get; set; }

        public IEnumerable<FarmDeviceSummary> devices { get; set; }
    }

    public class FarmDeviceSummary
    {
        public string name { get; set; }
        public string deviceModel { get; set; }

        public IEnumerable<string> sensors { get; set; }
    }
}
