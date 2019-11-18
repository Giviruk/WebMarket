using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{

    [Route("api/profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        public string url = "api/profile";
        private readonly d6h4jeg5tcb9d8Context _context;

        public ProfileController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
            
                return Ok(JsonConvert.SerializeObject(users));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if(user == null)
                {
                    return NotFound();
                }
                return Ok(JsonConvert.SerializeObject(user));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string[] value)
        {
            try
            {
                var user = await _context.Users.FindAsync(int.Parse(value[0]));
                var city = await _context.Cities.Select(x => x).Where(x => x.Name == value[3]).FirstOrDefaultAsync();

                if (city == null)
                    return BadRequest("City not found");

                user.Login = value[1];
                user.Pass = value[2];
                user.City = city.Id;
                user.Firstname = value[4];
                user.Middlename = value[5];
                user.Lastname = value[6];//may be null
                user.Addres = value[7];

                user.CityNavigation = _context.Cities.Find(user.City);

                _context.Users.Add(user);
                _context.SaveChanges();
                if(user == null)
                {
                    return BadRequest(HttpStatusCode.BadRequest);
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("auth")]
        public async Task<IActionResult> Post([FromBody]List<string> value)
        {
            try
            {
                var user = await _context.Users.Select(x => x).Where(x => x.Login == value[0]).FirstOrDefaultAsync();
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/values
        [HttpPut]
        public IActionResult Put([FromBody]string[] value)
        {
            try
            {
                var user = new Users();

                user.Login = value[0];
                user.Pass = value[1];
                user.City = int.Parse(value[2]);
                user.Firstname = value[3];
                user.Middlename = value[4];
                user.Lastname = value[5];//may be null
                user.Addres = value[6];

                user.CityNavigation = _context.Cities.Find(user.City);

                _context.Users.Add(user);
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
                _context.Users.Remove(_context.Users.Find(id));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
