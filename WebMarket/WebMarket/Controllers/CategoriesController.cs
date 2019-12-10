using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataClassLibrary;
using DataClassLibrary.DbContext;
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
        public  IActionResult UpdateCategory(int id, [FromBody]Category modifyedCategory)
        {
            var category = _context.Categories.Find(id);

            var jsoned = JsonConvert.SerializeObject(category);

            try
            {
                if (id != modifyedCategory.Id)
                    return BadRequest();

                category = modifyedCategory;
                _context.Categories.Update(category);
                _context.SaveChanges();

                //category.Name = modifyedCategory.Name;
                //category.Characteristics = modifyedCategory.Characteristics;
                //category.Product = modifyedCategory.Product;;
                //_context.SaveChanges();
                //_context.Entry(category).State = EntityState.Modified;

                //_context.Entry(modifyedCategory).State = EntityState.Modified;

                //_context.Categories.Update(modifyedCategory);
                //_context.SaveChanges();

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
        public  ActionResult<Category> PostCategories(Category categories)
        {
            _context.Categories.Add(categories);
            _context.SaveChanges();

            return CreatedAtAction("GetCategories", new { id = categories.Id }, categories);
        }

        // DELETE: api/Categories/5
        [HttpDelete("delete/{id}")]
        public  ActionResult<Category> DeleteCategories(int id)
        {
            try
            {
                var category =  _context.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);
                _context.SaveChanges();

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

                    var newCategoryId = _context.Categories.ToList().LastOrDefault().Id;

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
