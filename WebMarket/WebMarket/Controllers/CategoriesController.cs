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
            var selectedData = await _context.Categories.ToListAsync();

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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody]Category modifyedCategory)
        {
            try
            {
                if (id != modifyedCategory.Id)
                    return BadRequest();


                _context.Entry(modifyedCategory).State = EntityState.Modified;

                _context.Categories.Update(modifyedCategory);
                await _context.SaveChangesAsync();

                return Ok(modifyedCategory.Id);
            }
            catch (Exception ex)
            {
                if (!CategoriesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
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
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Category>> DeleteCategories(int id)
        {
            try
            {
                var categories = await _context.Categories.FindAsync(id);
                if (categories == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(categories);
                await _context.SaveChangesAsync();

                return Ok(id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            

        }

        private bool CategoriesExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromBody]Category newCategory)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (_context.Categories.Select(c => c.Name).Contains(newCategory.Name))
                        throw new Exception();

                    _context.Categories.Add(newCategory);
                    _context.SaveChanges();

                    var newCategoryId = _context.Categories.LastOrDefault();

                    transaction.Commit();
                    return Ok(newCategoryId);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex);
                }
            }
        }
    }
}
