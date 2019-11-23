using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{
    [Route("api/city")]
    public class CityController : Controller
    {
        private readonly d6h4jeg5tcb9d8Context _context;

        public CityController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }
        // GET: api/values
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Cities);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                return Ok(_context.Cities.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Cities value)
        {
            try
            {
                var city = _context.Cities.Find(value.Id);
                city.Name = value.Name;

                _context.Cities.Update(city);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Cities value)
        {
            try
            {
                var city = new Cities();
                city.Name = city.Name;
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _context.Cities.Remove(_context.Cities.Find(id));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
