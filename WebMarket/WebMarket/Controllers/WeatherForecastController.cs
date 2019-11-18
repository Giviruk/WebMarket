using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace WebMarket.Controllers
{
    [ApiController]
    [Route("/api")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var controllers = "https://webmarket911.herokuapp.com/api/categories/all \n" +
                "https://webmarket911.herokuapp.com/api/categories/{id} \n" +
                "https://webmarket911.herokuapp.com/api/Others \n" +
                "https://webmarket911.herokuapp.com/api/others/{id} \n" +
                "https://webmarket911.herokuapp.com/api/others/novelties \n" +
                "https://webmarket911.herokuapp.com/api/others/Bestsellers \n" +
                "https://webmarket911.herokuapp.com/api/productimages \n" +
                "https://webmarket911.herokuapp.com/api/productimages/{id} \n" +
                "https://webmarket911.herokuapp.com/api/products/all \n" +
                "https://webmarket911.herokuapp.com/api/products/product/{id} \n" +
                "https://webmarket911.herokuapp.com/api/products/category/{id} \n" +
                "https://webmarket911.herokuapp.com/api/profile \n" +
                "https://webmarket911.herokuapp.com/api/profile/{id} \n" +
                "https://webmarket911.herokuapp.com/api/profile/auth - post for auth \n";

            return controllers;
        }
    }
}
