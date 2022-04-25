using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenAQ.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenAQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Get all cities to be displayed for dropdown
            var cities = GetCities();
            var cityList = new List<SelectListItem>();
            foreach (var city in cities)
            {
                cityList.Add(new SelectListItem() { Text = city.city, Value = city.city });
            }

            ViewBag.Cities = cityList;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string GetCitiesFromAPI()
        {
            string data = string.Empty;

            try
            {
                //Fetch cities from API
                WebRequest request = WebRequest.Create("https://u50g7n0cbj.execute-api.us-east-1.amazonaws.com/v2/cities");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                data = reader.ReadLine();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception("Error: " + e.Message);
            }

            return data;
        }

        public List<Models.City.Result> GetCities()
        {
            string data = GetCitiesFromAPI();

            //Deserialize from JSON
            var jsonData = JsonSerializer.Deserialize<Models.City>(data);

            return jsonData.results.OrderBy(x => x.name).ToList();
        }

        [HttpPost]
        public IActionResult GetCityByName(Models.CityViewModel cityData)
        {
            string data = GetCitiesFromAPI();

            //Get city where name equals city name selected
            var jsonData = JsonSerializer.Deserialize<Models.City>(data).results.FirstOrDefault(x => x.city == cityData.City);

            //Convert to viewmodel for displaying City info
            var viewModel = new Models.CityViewModel(jsonData);


            //Get all cities to be displayed for dropdown
            var cities = GetCities();
            var cityList = new List<SelectListItem>();
            foreach (var city in cities)
            {
                cityList.Add(new SelectListItem() { Text = city.city, Value = city.city });
            }

            ViewBag.Cities = cityList;


            return View("Index", viewModel);
        }
    }
}
