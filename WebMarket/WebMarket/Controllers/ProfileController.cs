using System.Collections.Generic;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Users> Get()
        {
            var users = _context.Users.ToList();
            return users;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var user = _context.Users.Find(id);
            return JsonConvert.SerializeObject(user);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string[] value)
        {
            var user = _context.Users.Find(int.Parse(value[0]));

            user.Login = value[1];
            user.Pass = value[2];
            user.City = int.Parse(value[3]);
            user.Firstname = value[4];
            user.Middlename = value[5];
            user.Lastname = value[6];//may be null
            user.Addres = value[7];

            user.CityNavigation = _context.Cities.Find(user.City);

            _context.Users.Add(user);
            _context.SaveChanges();
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
