using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
string connString = config.GetConnectionString("DefaultConnection");

Console.WriteLine("Please enter the zip code of the area you would like to see the weather of (USA ONLY)");
var zipcode = Console.ReadLine();

var geoClient = new HttpClient();

var geoURL = $"http://api.openweathermap.org/geo/1.0/zip?zip={zipcode},US&appid={connString}";

var geoResponse = geoClient.GetStringAsync(geoURL).Result;

var lat = JObject.Parse(geoResponse).GetValue("lat").ToString();
var lon = JObject.Parse(geoResponse).GetValue("lon").ToString();

var WeatherClient = new HttpClient();

var WeatherURL = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={connString}&units=imperial";

var WeatherResponse = WeatherClient.GetStringAsync(WeatherURL).Result;

var WeatherMain = JObject.Parse(WeatherResponse);

Console.WriteLine(WeatherMain["main"]["temp"]);