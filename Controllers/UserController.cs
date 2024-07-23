using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using TravelBooking.MVC.Models;

namespace TravelBooking.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;

        private readonly string? _apiUrl;
        private readonly HttpClient _httpClient;
        public UserController(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
            _apiUrl = _config.GetValue<string>("WebApiUrl");

            // Ensure the API URL is not null or empty
            if (string.IsNullOrEmpty(_apiUrl))
            {
                throw new ArgumentException("API URL is not configured properly.");
            }

            // Initialize HttpClient if not provided
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.Timeout = TimeSpan.FromSeconds(_config.GetValue<int>("WebApiTimeOut"));
                _httpClient.BaseAddress = new Uri(_apiUrl);
            }
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<UserAccountViewModel> userAccountList = new List<UserAccountViewModel>();

            try
            {
                HttpResponseMessage response = _httpClient.GetAsync("api/GetUserAccounts").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var deserializedList = JsonConvert.DeserializeObject<List<UserAccountViewModel>>(data);
                    if (deserializedList != null)
                    {
                        userAccountList = deserializedList;
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }

            return View(userAccountList);
        }
    }
}
