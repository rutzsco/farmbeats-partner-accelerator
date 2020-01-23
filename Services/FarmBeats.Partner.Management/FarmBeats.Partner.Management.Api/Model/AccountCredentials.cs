using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class AccountCredentials
    {
        public string Url { get; set; }
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Resource { get; set; }

        public string Authority { get; set; }
    }
}
