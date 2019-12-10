using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FunctionLibraryFS;
using DataClassLibrary.DbContext;
using System;

namespace WebMarket.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AbstractDbContext _context;

        public ProductsController(AbstractDbContext context)
        {
            _context = context;
        }

        // GET: api/Products/all
        [HttpGet("all")]
        public async Task<ActionResult<string>> GetProduct()
        {
            var productsList = await _context.Products.ToListAsync();
            return JsonConvert.SerializeObject(productsList);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<string>> GetProduct(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return JsonConvert.SerializeObject(product);
        }


        // GET: api/Products/5
        [HttpGet("category/{id}")]
        public async Task<ActionResult<string>> GetProductsFromCategory(int categoryId)
        {
            var selectedProducts = await _context.Products
                .Where(p => p.Category == categoryId)
                .ToListAsync();

            return JsonConvert.SerializeObject(selectedProducts);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("update/{id}")]
        public IActionResult UpdateProduct(int id,[FromBody]Product modifiedProduct)
        {
            try
            {
                var product = _context.Categories.Find(id);

                if (modifiedProduct.Id != id || modifiedProduct.Category == null)
                    throw new ArgumentException();

                _context.Entry(modifiedProduct).State = EntityState.Modified;

                _context.SaveChanges();

                return Ok(modifiedProduct.Id);

            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("addProduct")]
        public  ActionResult<string> AddProduct([FromBody]Product newProduct)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (newProduct.Category == null)
                        throw new ArgumentException();

                    _context.Products.Add(newProduct);
                    _context.SaveChanges();

                    var newProductId = _context.Products.ToList().LastOrDefault();

                    transaction.Commit();
                    return Ok(newProductId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex);
                }
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("delete/{id}")]
        public  ActionResult<Product> DeleteProduct(int id)
        {
            var product =  _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
             _context.SaveChanges();

            return product;
        }
    }
}
