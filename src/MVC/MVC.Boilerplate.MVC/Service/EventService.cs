using MVC.Boilerplate.Models.Event;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Boilerplate.Service
{
    public class EventService
    {
        public static async Task<Events> GetEventList()
        {
            string Baseurl = "https://localhost:5000/api/v1/";
            Events events = new Events();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Baseurl) })
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = new();

                try
                {
                    Res = await client.GetAsync("Events");
                    if (Res.IsSuccessStatusCode)
                    { 
                        var EventResponse = Res.Content.ReadAsStringAsync().Result;

                        events = JsonConvert.DeserializeObject<Events>(EventResponse);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return events;
            }
        }
        public static async Task<EventDetails> CreateEvents(EventDetails eventt)
        {
            string Baseurl = "https://localhost:5000/api/v1/";
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Baseurl) })
            { 
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = new();

                string json = JsonConvert.SerializeObject(eventt);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                Res = await client.PostAsync("Events", httpContent);
                Res.EnsureSuccessStatusCode();
                var test = await Res.Content.ReadAsStringAsync();

                return eventt;
            }
        }
    }

    

}
