// Controllers/WeatherController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    public class WeatherController : Controller
    {
        private readonly WeatherService _weatherService;

        public WeatherController()
        {
            _weatherService = new WeatherService();
        }

        public async Task<IActionResult> Index()
        {
            var weatherData = await _weatherService.GetWeeklyWeather();
            return View(weatherData);
        }
    }
}
