using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataClassLibrary;
using DataClassLibrary.DbContext;
using FunctionLibraryFS;
using Microsoft.AspNetCore.Mvc.TagHelpers;

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
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("Novelties")]
        public string GetNovelties()
        {
            var random = new Random();
            var selectedProductsList = new List<Product>();

            var idList = _context.Products
                .Select(p => p.Id)
                .ToList();
            

            for(int i = 0; i<3;i++)
            {
                var val = random.Next();
                int index = idList[val % (idList.Count)];
                idList.Remove(index);
                selectedProductsList.Add(_context.Products.FirstOrDefault(p => p.Id == index));
            }

            return JsonConvert.SerializeObject(selectedProductsList);

        }

        [HttpGet("Bestsellers")]
        public ActionResult<Product> GetBestsellers()
        {
            try
            {

                var orderProducts = _context.OrderProducts.ToList();
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
