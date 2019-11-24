using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataClassLibrary;

namespace WebMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ITransient _td;
        private ISingleton _sd;
        private IScoped _scd;

        [HttpGet]
        public string Get()
        {
            return _td.GetString() + _sd.GetString() + _scd.GetString();
        }

        public WeatherForecastController(ISingleton sd, ITransient td, IScoped scd)
        {
            this._td = td;
            this._sd = sd;
            this._scd = scd;
        }     
    }
}