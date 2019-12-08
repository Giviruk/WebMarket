﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebMarket.Logic.AbstractContext;
using Data_Class_Library;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;

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
        [HttpGet("get/{userId}")]
        public ActionResult<string> GetUserOrders(int userId)
        {
            try
            {
                var userOrders = _context.Orders.Where(o => o.Owner == userId).ToList();

                var orderProducts = _context.OrderProducts
                    .Where(op => userOrders.Select(UserOrder => UserOrder.Id).Contains((int)op.Orderid))
                    .ToList();

                var statusCodes = _context.Statuses.ToList();
                var productIds = new List<int?>();

                foreach (var orderProduct in orderProducts)
                    if (!productIds.Contains(orderProduct.Productid))
                        productIds.Add(orderProduct.Id);

                var proudcts = _context.Products
                    .Where(p => orderProducts.Select(op => op.Productid).Contains(p.Id))
                    .ToList();

                return Ok(userOrders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/values
        [HttpPost("send")]
        public ActionResult<string> Post([FromBody]string orderWithProductListSerealized)
        {
            OrderWithProductList orderWithProductList = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderWithProductList>(orderWithProductListSerealized);

            var order = orderWithProductList.Order;
            var products = orderWithProductList.ProductIdList;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    int? userId =  order.Owner;
                    int orderId = _context.Orders.ToList().LastOrDefault().Id;

                    if (userId != null)
                    {
                        var userOdrderList = new List<UserOrder>();
                        var orderProductsList = new List<OrderProduct>();

                        foreach (var p in products)
                        {
                            orderProductsList.Add(new OrderProduct() { Orderid = orderId, Productid = p });
                        }

                        _context.OrderProducts.AddRange(orderProductsList);
                        _context.SaveChanges();

                        _context.UserOrders.Add(new UserOrder() { Userid = userId, Orderid = orderId });
                        _context.SaveChanges();
                    }

                    transaction.Commit();
                    return Ok(orderId);
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
