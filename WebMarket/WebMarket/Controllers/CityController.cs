using System;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Logic.AbstractContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{
    [Route("api/city")]
    public class CityController : Controller
    {
        private readonly AbstractDbContext _context;

        public CityController(AbstractDbContext context)
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
                var result = _context.Cities
                    .Where(c => c.Id == id)
                    .FirstOrDefault();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]City value)
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

        // PUT api/city
        [HttpPut]
        public IActionResult Put([FromBody]City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
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
