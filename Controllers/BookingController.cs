using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using TravelBooking.MVC.Models;

namespace TravelBooking.MVC.Controllers
{
    public class BookingController : Controller
    {
        //Uri baseAddress = new Uri("https://localhost:7020/api");

         private readonly IConfiguration _config;

        private readonly string? _apiUrl;
        private readonly HttpClient _httpClient; 

        public BookingController(IConfiguration config, HttpClient httpClient)
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
            List<BookingViewModel> bookingList = new List<BookingViewModel>();

            try
            {
                HttpResponseMessage response = _httpClient.GetAsync("api/Booking/GetBookings").Result;
                if (response.IsSuccessStatusCode)
                    if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    var deserializedList = JsonConvert.DeserializeObject<List<BookingViewModel>>(data);
                    if (deserializedList != null)
                    {
                        bookingList = deserializedList;
                    }
                }
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }

            return View(bookingList);
        }
    }
}
