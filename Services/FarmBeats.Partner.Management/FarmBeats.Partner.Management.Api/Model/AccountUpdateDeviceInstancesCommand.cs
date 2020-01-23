using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class AccountUpdateDeviceInstancesCommand
    {
        public AccountConfigurationSummary AccountConfiguration { get; set; }

        public AccountCredentials Credentials { get; set; }
    }
}
