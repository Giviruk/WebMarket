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
            var users = await _context.Users.ToListAsync();
            return JsonConvert.SerializeObject(users);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return JsonConvert.SerializeObject(user);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<HttpStatusCode>> Post([FromBody]string[] value)
        {
            var user = await _context.Users.FindAsync(int.Parse(value[0]));
            var city = await _context.Cities.Select(x => x).Where(x => x.Name == value[3]).FirstOrDefaultAsync();
            if (city == null)
                return HttpStatusCode.BadRequest;
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
            return HttpStatusCode.OK;
        }


        [HttpPost("auth")]
        public bool Post([FromBody]List<string> value)
        {
            var user = _context.Users.Select(x => x).Where(x => x.Login == value[0]).FirstOrDefault();
            return user != null && user.Pass == value[1];
        }

        // PUT api/values
        [HttpPut]
        public void Put([FromBody]string[] value)
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
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
