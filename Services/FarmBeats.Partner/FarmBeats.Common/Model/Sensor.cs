// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Common.Model
{
    public class Sensor
    {
        public Sensor(string hardwareId, string sensorModelId, string deviceId, string name)
        {
            this.hardwareId = hardwareId;
            this.sensorModelId = sensorModelId;
            this.deviceId = deviceId;
            this.name = name;
        }
        public string id { get; set; }

        public string hardwareId { get; set; }

        public string sensorModelId { get; set; }

        public string deviceId { get; set; }

        public string name { get; set; }

    }
}
