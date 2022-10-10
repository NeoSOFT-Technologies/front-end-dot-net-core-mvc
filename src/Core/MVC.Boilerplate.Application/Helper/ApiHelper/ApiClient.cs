using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Application.Helper.ApiHelper
{
    public class ApiClient<T>:IApiClient<T>
    {
        private HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient() { BaseAddress= new Uri(_configuration.GetSection("BaseUrl").Value) };
        }

        public async Task<IEnumerable<T>> GetAllAsync(string apiUrl)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(apiUrl);

            if (!responseMessage.IsSuccessStatusCode)
                await RaiseException(responseMessage);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(await responseMessage.Content.ReadAsStringAsync());
        }

        public async Task<T> GetByIdAsync(string apiUrl)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync(apiUrl);
            return await ValidateResponse(responseMessage);
        }

        public async Task<T> PostAsync(string apiUrl, T entity)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "text/plain");
            HttpResponseMessage responseMessage = await _httpClient.PostAsync(apiUrl, stringContent);
            return await ValidateResponse(responseMessage);
        }

        public async Task<T> PutAsync(string apiUrl, T entity)
        {
            apiUrl = apiUrl ?? string.Empty;

            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "text/plain");
            HttpResponseMessage responseMessage = await _httpClient.PutAsync(apiUrl, stringContent);
            return await ValidateResponse(responseMessage);
        }

        public async Task<string> DeleteAsync(string apiUrl)
        {
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync(apiUrl);
            if (!responseMessage.IsSuccessStatusCode)
                await RaiseException(responseMessage);
            
            return await responseMessage.Content.ReadAsStringAsync();

        }

        public void AddHeaders(Dictionary<string, string> headers)
        {
            foreach (KeyValuePair<string, string> header in headers)
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        async Task<T> ValidateResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                await RaiseException(response);   
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        async Task RaiseException(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            response.Content?.Dispose();
            throw new HttpRequestException($"{response.StatusCode}:{content}");
        }
    }
}
