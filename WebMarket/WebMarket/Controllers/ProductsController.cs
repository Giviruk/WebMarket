using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using FunctionLibraryFS;
using DataClassLibrary.DbContext;
using System;
using System.Collections.Generic;

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
        public async Task<ActionResult<string>> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return JsonConvert.SerializeObject(product);
        }

        [HttpGet("product/{id}/products")]
        public async Task<ActionResult<string>> GetPoductsWith(int id)
        {
            try
            {
                var orders = await _context.Orders.ToListAsync();
                var productsInOrders = new Dictionary<int, int>();
                foreach (var order in orders)
                {
                    var _products = order.Productinorder.Select(x => x.Id);
                    if (_products.Contains(id))
                    {
                        foreach (var p in _products)
                        {
                            productsInOrders[p] += 1;
                        }
                    }
                }
                var sortedDictionary = productsInOrders.OrderByDescending(x => x.Value).ToList();
                if (sortedDictionary.Count > 3)
                {
                    sortedDictionary = sortedDictionary.Take(3).ToList();
                }

                if (sortedDictionary.Count == 2)
                {
                    sortedDictionary = sortedDictionary.Take(2).ToList();
                }

                if (sortedDictionary.Count == 1)
                {
                    sortedDictionary.Add(sortedDictionary[0]);
                }

                if (sortedDictionary.Count == 0)
                {
                    return Ok("Нет товаров");
                }
                var products = new List<Product>();
                for (var i = 0; i < sortedDictionary.Count; i++)
                {
                    products.Add(_context.Products.Find(sortedDictionary[i].Key));
                }

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
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
                var product = _context.Products.Find(id);

                if (modifiedProduct.Id != id || modifiedProduct.Category == null)
                    throw new ArgumentException();

                product.Category = modifiedProduct.Category;
                product.CategoryNavigation = modifiedProduct.CategoryNavigation;
                product.Characteristics = modifiedProduct.Characteristics;
                product.Description = modifiedProduct.Description;
                product.Mainpictureurl = modifiedProduct.Mainpictureurl;
                product.MainpictureurlNavigation = modifiedProduct.MainpictureurlNavigation;
                product.Name = modifiedProduct.Name;
                product.OrderProducts = modifiedProduct.OrderProducts;
                product.Price = modifiedProduct.Price;
                product.Producer = modifiedProduct.Producer;
                product.ProductImages = modifiedProduct.ProductImages;
                product.ProductRating = modifiedProduct.ProductRating;
                product.Review = modifiedProduct.Review;              

                _context.SaveChanges();
                _context.Entry(modifiedProduct).State = EntityState.Modified;

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
