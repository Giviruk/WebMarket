using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DataClassLibrary;
using DataClassLibrary.DbContext;
using FunctionLibraryFS;
using System;
using Microsoft.FSharp.Core;

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


        [HttpGet("GetCategory/{categoryId}")]
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
            var result = FunctionLibraryFS.CategoryControllerFs.UpdateProduct(_context, id, modifyedCategory);

            if (FSharpOption<int>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
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
                var category = _context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
                var products = category.Products;

                foreach(var product in products)
                {
                    product.Status = "delete";
                }
                var productsId = products.Select(x => x.Id).ToList();

                var orders = _context.Orders.Include(x => x.Productinorder).ToList();
                foreach(var order in orders)
                {
                    foreach(var po in order.Productinorder)
                    {
                        var p = _context.Products.ToList();
                        if(po.Product.Status == "delete" && order.Status == 1)
                        {
                            order.Status = 5;
                        }
                    }
                }

                _context.Orders.UpdateRange(orders);
                _context.SaveChanges();

                //var categroyProducts = _context.Products.ToList().Where(p => p.Category.Value == category.Id).ToList();

                //var productsIds = categroyProducts.Select(cp => cp.Id).ToList();

                //var productImages = _context.ProductImages.ToList().Where(pi => productsIds.Contains(pi.Productid.Value)).ToList();

                //foreach(var i in productImages)
                //{
                //    _context.ProductImages.Remove(i);
                //    _context.SaveChanges();
                //}

                //var productOrder = _context.OrderProducts.ToList().Where(op => productsId.Contains(op.Productid.Value)).ToList();

                //foreach (var po in productOrder)
                //{
                //    _context.OrderProducts.Remove(po);
                //    _context.SaveChanges();
                //}

                //var productReviews = _context.Reviews.ToList().Where(r => productsIds.Contains(r.ProductId)).ToList();

                //foreach(var r in productReviews)
                //{
                //    _context.Reviews.Remove(r);
                //    _context.SaveChanges();
                //}

                //foreach(var cp in categroyProducts)
                //{
                //    _context.Products.Remove(cp);
                //    _context.SaveChanges();
                //}


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

                    var newCategoryId = _context.Categories.ToList().Select(c => c.Id).Max();

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
