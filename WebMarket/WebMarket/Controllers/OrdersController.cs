using System;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{
    [Route("api/order")]
    public class OrdersController : Controller
    {

        private readonly d6h4jeg5tcb9d8Context _context;

        public OrdersController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult<Orders> Get()
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
        public ActionResult<Orders> Get(int id)
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
        public void Post([FromBody]Orders value)
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
