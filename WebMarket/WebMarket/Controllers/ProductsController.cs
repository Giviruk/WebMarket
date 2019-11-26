using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FunctionLibraryFS;

namespace WebMarket.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly d6h4jeg5tcb9d8Context _context;

        public ProductsController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }

        // GET: api/Products/all
        [HttpGet("all")]
        public async Task<ActionResult<string>> GetProduct()
        {
            var productsList = await _context.Product.ToListAsync();
            return JsonConvert.SerializeObject(productsList);
        }

        [HttpGet("product/{id}")]
        public async Task<ActionResult<string>> GetProduct(int productId)
        {
            var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == productId);
            return JsonConvert.SerializeObject(product);
        }


        // GET: api/Products/5
        [HttpGet("category/{id}")]
        public async Task<ActionResult<string>> GetProductsFromCategory(int categoryId)
        {
            var selectedProducts = await _context.Product
                .Where(p => p.Category == categoryId)
                .ToListAsync();

            var result = Say.GetProductsFromCategoryFS(_context.Product, categoryId);

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
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
