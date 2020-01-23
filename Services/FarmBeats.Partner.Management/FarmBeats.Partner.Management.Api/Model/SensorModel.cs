// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace FarmBeats.Partner.Management.Api.Model
{
    public class ListingResponse<T>
    {
        public IEnumerable<T> items { get; set; }
    }

    public class SensorModel
    {
        
        public string id { get; set; }

        public string type { get; set; }

        public string productCode { get; set; }

        public string name { get; set; }

        public IEnumerable<SensorMeasure> SensorMeasures { get; set; }
    }

    public class SensorMeasure
    {


        public SensorMeasure(string name, string dataType, string type, string unit)
        {
            this.name = name;
            this.dataType = dataType;
            this.type = type;
            this.unit = unit;
        }

        public string name { get; set; }

        public string dataType { get; set; }

        public string type { get; set; }

        public string unit { get; set; }
    }
}
