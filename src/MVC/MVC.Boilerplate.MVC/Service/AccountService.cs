using MVC.Boilerplate.Application.Helper.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Account;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Boilerplate.Service
{
    public class AccountService: IAccountService
    {
        private readonly IApiClient<LoginResponse> _apiClientLogin;
        private readonly IApiClient<Register> _apiClientRegister;
        private readonly ILogger<AccountService> _logger;


        public AccountService(IApiClient<LoginResponse> apiClientLogin, ILogger<AccountService> logger, IApiClient<Register> apiClientRegister)
        {
            _apiClientLogin = apiClientLogin;
            _logger = logger;
            _apiClientRegister = apiClientRegister;
        }
        //public static async Task<LoginResponse> Login(Login login)
        //{



        //    //string Baseurl = "https://localhost:44330/api/v1/";
        //    //LoginResponse response = new LoginResponse();
        //    //var httpClientHandler = new HttpClientHandler();
        //    //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
        //    //{
        //    //    return true;
        //    //};
        //    //using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Baseurl) })
        //    //{
        //    //    //Passing service base url  
        //    //    client.DefaultRequestHeaders.Clear();
        //    //    //Define request data format  
        //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //    HttpResponseMessage Res = new();
        //    //    try
        //    //    {
        //    //        string json = JsonConvert.SerializeObject(login);
        //    //        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //    //        Res = await client.PostAsync("Account/authenticate", httpContent);
        //    //        Res.EnsureSuccessStatusCode();
        //    //        var ResJsonString = await Res.Content.ReadAsStringAsync();
        //    //        response = JsonConvert.DeserializeObject<LoginResponse>(ResJsonString);
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        response.Message = $"Credentials for  { login.Email}  arent valid.";
        //    //    }

        //    //    return response;
        //    }
        //}

        public async Task<LoginResponse> Login(Login login)
        {
            _logger.LogInformation("LoginAccount Service initiated");
            var response = await _apiClientLogin.PostAuthAsync("Account/authenticate", login);
            if(response == null)
            {
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.Message = $"Credentials for  { login.Email}  arent valid.";
                _logger.LogInformation("LoginAccount Service conpleted");
                return loginResponse;
            }
            _logger.LogInformation("LoginAccount Service conpleted");
            return response;
        }
        public async Task<Register> Register(Register register)
        {
            _logger.LogInformation("RegisterAccount Service initiated");
            var response = await _apiClientRegister.PostAuthAsync("Account/register", register);
            if (response == null)
            {
                //Register registerResponse = new Register();
                register.Message = $" UserName {register.UserName} or Email {register.Email} already exist";
                return register;
            }
            _logger.LogInformation("RegisterAccount Service conpleted");
            return response;

        }

        //public static async Task<Register> Register(Register register)
        //{
        //    string Baseurl = "https://localhost:44330/api/v1/";
        //    var httpClientHandler = new HttpClientHandler();
        //    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
        //    {
        //        return true;
        //    };
        //    using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(Baseurl) })
        //    {
        //        //Passing service base url  
        //        client.DefaultRequestHeaders.Clear();
        //        //Define request data format  
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        HttpResponseMessage Res = new();

        //        try
        //        {
        //            string json = JsonConvert.SerializeObject(register);
        //            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        //            Res = await client.PostAsync("Account/register", httpContent);
        //            Res.EnsureSuccessStatusCode();
        //        }
        //        catch (Exception ex)
        //        {
        //            register.Message = "Something";
        //        }

        //        return register;
        //    }
        //}
    }
}
