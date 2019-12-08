using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using WebMarket.Logic.AbstractContext;
using WebMarket.Logic.Email;
namespace WebMarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ITransient _td;
        private ISingleton _sd;
        private IScoped _scd;

        private readonly AbstractDbContext _context;
        public WeatherForecastController(AbstractDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public string Get()
        {
            //EmailSender.SendEmail("rehsatart@gmail.com", "Работяга");
            var res = JsonConvert.SerializeObject(_context.Categories.FirstOrDefault());
            return /*_td.GetString() + _sd.GetString() + _scd.GetString() +"\n"*/  res;
        }

        //public WeatherForecastController(ISingleton sd, ITransient td, IScoped scd)
        //{
        //    this._td = td;
        //    this._sd = sd;
        //    this._scd = scd;
        //}     
    }
}