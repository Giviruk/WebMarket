using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebMarket.Controllers
{
    [Route("api/Productimages")]
    [ApiController]
    public class ProductimagesController : ControllerBase
    {
        private readonly d6h4jeg5tcb9d8Context _context;

        public ProductimagesController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }

        // GET: api/Productimages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Productimages>>> GetProductimages()
        {
            return await _context.Productimages.ToListAsync();
        }

        // GET: api/Productimages/GetProductimages?productId=значение
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetProductimages(int productId)
        {
            var productImages = _context.Productimages
                .Where(pI => pI.Productid == productId)
                .Select(pI => pI.Imageid);

            var imagesReference = await _context.Images
                .Where(image => productImages.Contains(image.Id))
                .Select(image => image.Imagepath)
                .ToListAsync();

            return JsonConvert.SerializeObject(imagesReference);
        }

        // PUT: api/Productimages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductimages(int id, Productimages productimages)
        {
            if (id != productimages.Id)
            {
                return BadRequest();
            }

            _context.Entry(productimages).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductimagesExists(id))
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

        // POST: api/Productimages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Productimages>> PostProductimages(Productimages productimages)
        {
            _context.Productimages.Add(productimages);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductimages", new { id = productimages.Id }, productimages);
        }

        // DELETE: api/Productimages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Productimages>> DeleteProductimages(int id)
        {
            var productimages = await _context.Productimages.FindAsync(id);
            if (productimages == null)
            {
                return NotFound();
            }

            _context.Productimages.Remove(productimages);
            await _context.SaveChangesAsync();

            return productimages;
        }

        private bool ProductimagesExists(int id)
        {
            return _context.Productimages.Any(e => e.Id == id);
        }
    }
}
