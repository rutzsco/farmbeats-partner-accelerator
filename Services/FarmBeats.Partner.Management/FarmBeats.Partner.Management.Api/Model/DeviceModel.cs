// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class DeviceModel
    {
        
        public DeviceModel()
        {
        }
        public DeviceModel(string type, string manufacturer, string name, string description)
        {
            this.id = "2421c7bc-0ca5-41b1-ab82-fa1c2081c889";
            this.type = type;
            this.manufacturer = manufacturer;
            this.name = name;
            this.description = description;
        }

        public string id { get; set; }
        public string type { get; set; }
        public string manufacturer { get; set; }
        public string name { get; set; }
        public string description { get; set; }

    }
}
