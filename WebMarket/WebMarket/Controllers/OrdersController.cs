using System;
using System.Collections.Generic;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;
using DataClassLibrary.Logic.Email;
using System.Text;
using Microsoft.FSharp.Core;

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
        [HttpGet("getFromId/{userId}")]
        public ActionResult<string> GetUserOrdersFromUserId(int userId)
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

        [HttpPost("getFromEmail")]
        public ActionResult<string> GetUserOrderFromEmail([FromBody]string userEmail)
        {
            try
            {
                if (_context.Users.Where(u => u.Login == userEmail).Count() != 0)
                    return BadRequest(null);

                var ordersList = _context.Orders.Where(o => o.Email == userEmail).ToList();

                var orderId = ordersList.ToList().FirstOrDefault().Id;

                var orderProducts = _context.OrderProducts
                    //.Where(op => op.Orderid == orderId)
                    .ToList();

                var statusCodes = _context.Statuses.ToList();
                var productIds = new List<int?>();

                foreach (var orderProduct in orderProducts)
                    if (!productIds.Contains(orderProduct.Productid))
                        productIds.Add(orderProduct.Id);


                var proudcts = _context.Products
                    .Where(p => orderProducts.Select(op => op.Productid).Contains(p.Id))
                    .ToList();

                return Ok(ordersList);
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
                    var userOdrderList = new List<UserOrder>();
                    var orderProductsList = new List<OrderProduct>();

                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    int? userId =  order.Owner;
                    int orderId = _context.Orders.ToList().LastOrDefault().Id;

                    foreach (var p in products)
                    {
                        orderProductsList.Add(new OrderProduct() { Orderid = orderId, Productid = p });
                    }

                    _context.OrderProducts.AddRange(orderProductsList);
                    _context.SaveChanges();

                    if (userId != null)
                    { 
                        _context.UserOrders.Add(new UserOrder() { Userid = userId, Orderid = orderId });
                        _context.SaveChanges();
                    }

                    transaction.Commit();

                    var result = _context.Products.Where(p => products.Contains(p.Id)).Select(p => p.Name).Distinct();

                    var messageBody = new StringBuilder();

                    messageBody.Append($"Ваш заказ по номеру - {orderId} оформлен.\n\n\n");

                    foreach(var p in result)
                    {
                        messageBody.Append($"{p} \n");
                    }

                    messageBody.Append("\nСпасибо за покупку!");

                    EmailSender.SendEmail(order.Email, "Уважаемый покупатель \n",messageBody.ToString(), "Заказ из интернет магазина");
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
        //public IActionResult Put([FromBody]Order value)
        //{
        //    try
        //    {
        //        _context.Orders.Add(value);
        //        _context.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e);
        //    }
        //}

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

        //[HttpGet("allOrders")]
        //public 
        [HttpGet("getOrderFromOrderId/{orderId}")]
        public IActionResult GetOrderFromOrderId(int orderId)
        {
            var result = FunctionLibraryFS.OrdersControllerFs.GetOrderFromOrderId(_context, orderId);

            if (FSharpOption<Order>.get_IsSome(result.Value))
                return Ok(result.Value);
            else
                return BadRequest();
        }

        [HttpGet("getAll")]
        public IActionResult GetAllOrders()
        {
            var result = FunctionLibraryFS.OrdersControllerFs.GetAllOrders(_context);

            if (FSharpOption<List<Order>>.get_IsSome(result.Value))
                return Ok(result.Value);
            else
                return BadRequest();


        }
    }
}
