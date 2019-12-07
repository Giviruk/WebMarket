using System;
using System.Collections.Generic;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Logic.AbstractContext;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebMarket.Controllers
{
    [Route("api/orders")]
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
                return Ok();
            }
            catch (Exception ex)
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
                if (order == null)
                {
                    NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost("send")]
        public IActionResult Post([FromBody]Order order, [FromBody]List<int> products)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    int userId = (int)order.Owner;
                    int orderId = _context.Orders.ToList().LastOrDefault().Id;

                    var userOdrderList = new List<UserOrder>();
                    var orderProductsList = new List<OrderProduct>();

                    foreach (var p in products)
                    {
                        orderProductsList.Add(new OrderProduct() { Orderid = orderId, Productid = p });
                    }

                    _context.OrderProducts.AddRange(orderProductsList);
                    _context.SaveChanges();

                    _context.SaveChanges();
                    _context.UserOrders.Add(new UserOrder() { Userid = userId, Orderid = orderId });

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex);
                }
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Order value)
        {
            try
            {
                _context.Orders.Add(value);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var order = _context.Orders.Find((id));
                _context.Orders.Remove(order);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
