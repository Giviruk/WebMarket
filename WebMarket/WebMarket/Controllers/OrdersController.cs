using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("/product/{id}")]
        public string GetProduct(int id)
        {
            var _product = _context.Product.Find(id);
            var product = new ProductInOrder(_product);
            return JsonConvert.SerializeObject(product);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
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

    public class ProductInOrder
    {
        public ProductInOrder(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Category = product.Category;
            Producer = product.Producer;
            Mainpictureurl = product.Mainpictureurl;
            CategoryNavigation = product.CategoryNavigation;
            MainpictureurlNavigation = product.MainpictureurlNavigation;
            Orders = product.Orders;
            Productimages = product.Productimages;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public int? Category { get; set; }
        public string Producer { get; set; }
        public int? Mainpictureurl { get; set; }

        [JsonIgnore]
        public virtual Categories CategoryNavigation { get; set; }
        public virtual Images MainpictureurlNavigation { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Productimages> Productimages { get; set; }
    }
}
