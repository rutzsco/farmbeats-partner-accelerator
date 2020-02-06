// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Common.Model
{
    public class Device
    {
        public Device(string hardwareId, string deviceModelId, string farmId, Location location, string name, int reportingInterval)
        {
            this.hardwareId = hardwareId;
            this.deviceModelId = deviceModelId;
            this.farmId = farmId;
            this.location = location;
            this.name = name;
            this.reportingInterval = reportingInterval;
        }

        public string id { get; set; }

        public string hardwareId { get; set; }

        public string deviceModelId { get; set; }

        public string farmId { get; set; }

        public int reportingInterval { get; set; }
        
        public Location location { get; set; }

        public string name { get; set; }
    }

    public class Location
    {
        public Location(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double latitude { get; set; }

        public double longitude { get; set; }
    }
}
