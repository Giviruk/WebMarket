﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataClassLibrary.Logic.Hashing;
using DataClassLibrary.DbContext;
using Microsoft.FSharp.Core;

namespace WebMarket.Controllers
{

    [Route("api/profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        public string url = "api/profile";
        private readonly AbstractDbContext _context;

        public ProfileController(AbstractDbContext context)
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
            
                return Ok(users);
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
                var userCity = _context.Cities.Where(c => c.Id == user.City).FirstOrDefault();
                user.CityNavigation = userCity;
                if(user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]User value)
        {
            try
            {
                var user = await _context.Users.FindAsync(value.Id);
                //var city = await _context.Cities.Select(x => x).Where(x => x.Name == value[3]).FirstOrDefaultAsync();

                //if (city == null)
                //    return BadRequest("City not found");

                user.Login = value.Login;
                user.Pass = value.Pass;
                user.City = value.City;
                user.Firstname = value.Firstname;
                user.Middlename = value.Middlename;
                user.Lastname = value.Lastname;//may be null
                user.Address = value.Address;

                user.CityNavigation = _context.Cities.Find(user.City);

                _context.Users.Update(user);
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
        public async Task<IActionResult> Post([FromBody]string value)
        {
            try
            {
                var user = await _context.Users.Select(x => x).Where(x => x.Login == value).FirstOrDefaultAsync();
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/values
        [HttpPut]
        public  IActionResult RegiserUser([FromBody]User value)
        {
            try
            {
                var user = new User();
                //var city = await _context.Cities.Select(x => x).Where(x => x.Name == value[3]).FirstOrDefaultAsync();

                //if (city == null)
                //    return BadRequest("City not found");

                user.Login = value.Login;
                user.Pass = value.Pass;
                user.City = value.City;
                user.Firstname = value.Firstname;
                user.Middlename = value.Middlename;
                user.Lastname = value.Lastname;//may be null
                user.Address = value.Address;
                user.Token = Hash.MakeHash(value.Login);

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


        [HttpPost("generateKod")]
        public IActionResult GeneratePKod([FromBody]string userEmail)
        {
            var result = FunctionLibraryFS.ProfileControllerFs.GeneratePasswordChangeKod(userEmail);

            if (FSharpOption<string>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
        }

        [HttpPost("changePassword")]
        public IActionResult ChangePassword([FromBody]string[] userData)
        {
            var userEmail = userData[0];
            var userPassword = userData[1];

            var result = FunctionLibraryFS.ProfileControllerFs.ChangePassword(_context, userEmail, userPassword);

            if (FSharpOption<int>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
        }
    }
}
