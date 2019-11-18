using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebMarket.Controllers
{
    [ApiController]
    [Route("/api")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var controllers = new List<string>();
            controllers.Add("https://webmarket911.herokuapp.com/api/categories/all");
            controllers.Add("https://webmarket911.herokuapp.com/api/categories/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/Others");
            controllers.Add("https://webmarket911.herokuapp.com/api/others/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/others/novelties");
            controllers.Add("https://webmarket911.herokuapp.com/api/others/Bestsellers");
            controllers.Add("https://webmarket911.herokuapp.com/api/productimages");
            controllers.Add("https://webmarket911.herokuapp.com/api/productimages/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/products/all");
            controllers.Add("https://webmarket911.herokuapp.com/api/products/product/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/products/category/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/profile");
            controllers.Add("https://webmarket911.herokuapp.com/api/profile/{id}");
            controllers.Add("https://webmarket911.herokuapp.com/api/profile/auth - post for auth");

            return controllers;
        }
    }
}
