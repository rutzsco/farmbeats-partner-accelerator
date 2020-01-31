using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class Account
    {
        public string name { get; set; }

        public string Url { get; set; }

        public string ClientId { get; set; }

        public string ClientSecretSettingName { get; set; }

        public string Resource { get; set; }

        public string Authority { get; set; }


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
    }
}
