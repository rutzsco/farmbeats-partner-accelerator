// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using FarmBeats.Partner.Management.Api.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FarmBeats.Partner.Management.Api.Services
{
    public class FarmBeatsClient
    {
        private HttpClient _httpClient;
        private string _farmBeatsUrl;

        public FarmBeatsClient(string farmBeatsUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _farmBeatsUrl = farmBeatsUrl;
        }

        public async Task<DeviceModel> GetDeviceModel(string name)
        {
            var list = await GetList<DeviceModel>("DeviceModel");
            if (list.Any(x => x.name == name))
                return list.First(x => x.name == name);

            return null;
        }
        public async Task<DeviceModel> CreateDeviceModel(DeviceModel deviceModel)
        {
            var list = await GetList<DeviceModel>("DeviceModel");
            if (list.Any(x => x.name == deviceModel.name))
                return list.First(x => x.name == deviceModel.name);

            var response = await Post(deviceModel, "DeviceModel");
            return response;
        }
        public async Task<SensorModel> GetSensorModel(string name)
        {
            var list = await GetList<SensorModel>("SensorModel");
            if (list.Any(x => x.name == name))
                return list.Single(x => x.name == name);

            return null;
        }
        public async Task<SensorModel> CreateSensorModel(SensorModel sensorModel)
        {
            var list = await GetList<SensorModel>("SensorModel");
            if (list.Any(x => x.name == sensorModel.name))
                return list.Single(x => x.name == sensorModel.name);

            var response = await Post(sensorModel, "SensorModel");
            return response;
        }

        public async Task<Farm> GetFarm(string name)
        {
            var list = await GetList<Farm>("Farm");
            return list.First(x => x.name == name);
        }

        public async Task<Device> GetDevice(string name)
        {
            var list = await GetList<Device>("Device");
            return list.First(x => x.name == name);
        }

        public async Task<Device> CreateDevice(Device device)
        {
            var list = await GetList<Device>("Device");
            if (list.Any(x => x.name == device.name))
                return list.First(x => x.name == device.name);

            var response = await Post(device, "Device");
            return response;
        }

        public async Task<Sensor> CreateSensor(Sensor sensor)
        {
            var list = await GetList<Sensor>("Sensor");
            if (list.Any(x => x.name == sensor.name))
                return list.First(x => x.name == sensor.name);

            var response = await Post(sensor, "Sensor");
            return response;
        }

        private async Task<T> Post<T>(T request, string resource)
        {
            var url = _farmBeatsUrl + resource;
            var payload = JsonConvert.SerializeObject(request);
            var response = await _httpClient.PostAsync(url, new StringContent(payload, Encoding.UTF8, "application/json"));
            var responsePayload = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responsePayload);
        }

        private async Task<List<T>> GetList<T>(string resource)
        {
            var url = _farmBeatsUrl + resource;
            var response = await _httpClient.GetAsync(url);
            var responsePayload = await response.Content.ReadAsStringAsync();
            var listingResponse = JsonConvert.DeserializeObject<ListingResponse<T>>(responsePayload);
            return listingResponse.items.ToList();
        }
    }
}
