using System.Collections.Generic;

namespace OpenAQ.Models
{
    public class CityViewModel
    {
        public string Country { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Count { get; set; }
        public int Locations { get; set; }
        public List<string> Parameters { get; set; }


        public CityViewModel() {}
        public CityViewModel(Models.City.Result cityObject)
        {
            Country = cityObject.country;
            Name = cityObject.name;
            City = cityObject.city;
            Count = cityObject.count;
            Locations = cityObject.locations;
            Parameters = cityObject.parameters;
        }
    }
}
