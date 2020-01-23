using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class Device
    {
        public Device(string hardwareId, string deviceModelId, string farmId, Location location, string name)
        {
            this.hardwareId = hardwareId;
            this.deviceModelId = deviceModelId;
            this.farmId = farmId;
            this.location = location;
            this.name = name;
        }

        public string id { get; set; }

        public string hardwareId { get; set; }

        public string deviceModelId { get; set; }

        public string farmId { get; set; }

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
