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
            var images = _context.ProductImages.Where(x=>x.Productid==product.Id).ToList();
            foreach (var image in images)
            {
                image.Image = _context.Images.FirstOrDefault(x => x.Id == image.Id);
            }

            product.MainpictureurlNavigation = _context.Images.Find(product.Mainpictureurl);
            product.ProductImages = images;
            return Ok(product);
        }

        [HttpGet("product/{id}/products")]
        public ActionResult<string> GetPoductsWith(int id)
        {
            var allProductOrders = _context.OrderProducts.ToList();
            var productInOrder = _context.OrderProducts.Where(op => op.Productid == 7).ToList();
            var ordersWithRequestedProduct = _context.Orders.Where(order => productInOrder.Select(op => op.Orderid).Contains(order.Id)).ToList();
            var orderProductsWhichBoughtWithRequest = ordersWithRequestedProduct.Select(x => x.Productinorder).SelectMany(x => x).Where(x => x.Productid != id).ToList();
            var productIds = orderProductsWhichBoughtWithRequest.Select(op => op.Productid).ToList();
            var dict = new Dictionary<int, int>();
            foreach (int productId in productIds)
            {
                if (!dict.ContainsKey((int)productId))
                    dict.Add(productId, 1);
                else
                    dict[productId]++;
            }
            var requestedIds = dict.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).Take(3).ToList();
            var result = new List<Product>();
            foreach (var reqId in requestedIds)
                result.Add(_context.Products.Find(reqId));
            var images = _context.Images.ToList();
            var images2 = _context.ProductImages.ToList();
            return JsonConvert.SerializeObject(result);
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
