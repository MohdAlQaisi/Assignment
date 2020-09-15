using Assignment.Extensions;
using Assignment.Interfaces;
using Assignment.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Assignment.Services
{
    public class SvcApplication : ISvcApplication
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public SvcApplication(HttpClient client, IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
            _httpClient = client;
            _httpClient.BaseAddress = new Uri(_appSettings.BaseAddress);
            _httpClient.DefaultRequestHeaders.Add("Authorization", @$"Basic {_appSettings.RestAPIKey}");
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Application> GetAllApplication()
        {
            var response = _httpClient.GetAsync(_appSettings.EndPoint).Result;
            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<List<Application>>(response.Content.ReadAsStringAsync().Result);
                return model;
            }
            else
                return new List<Application>();
        }

        public Application CreateApplication(Application application)
        {
            var data = application.ConvertToSerlizedString();
            var response = _httpClient.PostAsync(_appSettings.EndPoint, data).Result;
            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<Application>(response.Content.ReadAsStringAsync().Result);
                return model;
            }
            else
                return new Application();
        }

        public Application GetApplicationById(string id)
        {
            var response = _httpClient.GetAsync($@"{_appSettings.EndPoint}/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<Application>(response.Content.ReadAsStringAsync().Result);
                return model;
            }
            else
                return default;
        }

        public Application EditApplication(Application application)
        {
            var data = application.ConvertToSerlizedString();
            var response = _httpClient.PutAsync(@$"{_appSettings.EndPoint}/{application.Id}", data).Result;
            if (response.IsSuccessStatusCode)
            {
                var model = JsonConvert.DeserializeObject<Application>(response.Content.ReadAsStringAsync().Result);
                return model;
            }
            else
                return default;
        }
    }
}
