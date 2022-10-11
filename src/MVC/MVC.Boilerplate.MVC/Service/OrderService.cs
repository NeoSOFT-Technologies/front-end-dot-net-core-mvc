using MVC.Boilerplate.Models.Order;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace MVC.Boilerplate.Service
{
    public class OrderService
    {
        public static async Task<Orders> GetKeyList()
        {
            string Baseurl = "https://localhost:44330/api/v1/";
            Orders orders = new Orders();
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Baseurl) })
            {
                //Passing service base url  
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = new();

                try
                {
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    Res = await client.GetAsync("Order?date=2022-02-21&page=1&size=8");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var OrderResponse = Res.Content.ReadAsStringAsync().Result;

                        orders = JsonConvert.DeserializeObject<Orders>(OrderResponse);
                    } 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return orders;
            }
        }
    }
}
