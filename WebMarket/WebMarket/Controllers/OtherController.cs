using DataClassLibrary;
using DataClassLibrary.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMarket.Controllers
{
    [Route("api/Others")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly AbstractDbContext _context;

        public OtherController(AbstractDbContext context)
        {
            _context = context;
        }

        // GET: api/Products1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {   
            return await _context.Products.Where(x=>x.Status!="delete").ToListAsync();
        }

        // GET: api/Products1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null || product.Status == "delete")
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("Novelties")]
        public string GetNovelties()
        {
            var lastProducts = _context.Products.OrderByDescending(p => p.Id).Where(x=>x.Status != "delete").Take(3).ToList();

            return JsonConvert.SerializeObject(lastProducts);

        }

        [HttpGet("Bestsellers")]
        public ActionResult<Product> GetBestsellers()
        {
            try
            {

                var orderProducts = _context.OrderProducts.Include(x=>x.Product).Where(x=>x.Product.Status != "delete").ToList();
                var dicOrderProduct = new Dictionary<int?, int?>();
                foreach (var product in orderProducts)
                {
                    if (!dicOrderProduct.Keys.Contains(product.Productid))
                    {
                        dicOrderProduct.Add(product.Productid, 1);
                    }
                    else
                    {
                        dicOrderProduct[product.Productid] += 1;
                    }
                }

                var topProductsId = dicOrderProduct.OrderByDescending(x => x.Value).Select(x => x.Key).ToList().Take(3);
                var topProduct = new List<Product>();
                foreach (var productid in topProductsId)
                {
                    topProduct.Add(_context.Products.Find(productid));
                }

                return Ok(topProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("GetUserFromToken")]
        public string GetUser([FromBody]string token)
        {
            var user = _context.Users.FirstOrDefault(u => u.Token == token);

            return JsonConvert.SerializeObject(user);
        }
    }
}
