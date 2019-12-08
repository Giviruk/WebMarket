using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataClassLibrary;
using WebMarket.Logic.AbstractContext;
using System;

namespace WebMarket.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AbstractDbContext _context;

        public CategoriesController(AbstractDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories/all
        [HttpGet("all")]
        public async Task<ActionResult<string>> GetCategories()
        {
            var selectedData =  await _context.Categories.ToListAsync();

            return JsonConvert.SerializeObject(selectedData);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetCategory(int categoryId)
        {
            var categories = await _context.Categories.FindAsync(categoryId);

            if (categories == null)
            {
                return NotFound();
            }

            return JsonConvert.SerializeObject(categories);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategories(int id, Category categories)
        {
            if (id != categories.Id)
            {
                return BadRequest();
            }

            _context.Entry(categories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriesExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategories(Category categories)
        {
            _context.Categories.Add(categories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", new { id = categories.Id }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategories(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if (categories == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();

            return categories;
        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult CreateCategory([FromBody]Category newCategory)
        {
            try
            {
                //if(_context)

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
