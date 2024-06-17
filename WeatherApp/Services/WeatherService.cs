// Services/WeatherService.cs
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        private readonly string url = "https://weather.com/weather/tenday/l/North+Bethesda+MD?canonicalCityId=91171ab3962dff96da9a2b2510c7c66f9886f02acc6756680f0fba9a22a8b0fe";

        public async Task<List<Weather>> GetWeeklyWeather()
        {
            var weatherList = new List<Weather>();

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(response);

                var nodes = htmlDocument.DocumentNode.SelectNodes("//details[@data-testid='daypartDetails']");

                foreach (var node in nodes.Take(7)) // Get only the first 7 days
                {
                    var day = node.SelectSingleNode(".//h3[@data-testid='daypartName']").InnerText.Trim();
                    var date = node.SelectSingleNode(".//h2[@data-testid='daypartDate']").InnerText.Trim();
                    var description = node.SelectSingleNode(".//span[@data-testid='daypartPhrase']").InnerText.Trim();
                    var highTemp = node.SelectSingleNode(".//span[@data-testid='TemperatureValue']").InnerText.Trim();
                    var lowTemp = node.SelectSingleNode(".//span[@data-testid='lowTempValue']").InnerText.Trim();

                    weatherList.Add(new Weather
                    {
                        Day = day,
                        Date = date,
                        Description = description,
                        HighTemp = highTemp,
                        LowTemp = lowTemp
                    });
                }
            }

            return weatherList;
        }
    }
}
