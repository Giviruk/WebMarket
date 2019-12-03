using System;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Logic.AbstractContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{
    [Route("api/order")]
    public class OrdersController : Controller
    {

        private readonly AbstractDbContext _context;

        public OrdersController(AbstractDbContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<Order> Get()
        {
            try
            {
                var orders = _context.Orders.ToList();
                if (orders == null)
                    return NotFound();
                return Ok(orders);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Order> Get(int id)
        {
            try
            {
                var order = _context.Orders.Find(id);
                if(order == null)
                {
                    NotFound();
                }
                return Ok(order);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Order value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
