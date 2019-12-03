using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FunctionLibraryFS;
using WebMarket.Logic.AbstractContext;

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

            var result = ProductControllerFs.GetProductsFromCategoryFS(_context.Products, categoryId);

            return JsonConvert.SerializeObject(selectedProducts);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
