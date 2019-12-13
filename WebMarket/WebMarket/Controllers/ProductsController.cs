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
using Microsoft.FSharp.Core;

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

        //FSharp work
        [HttpGet("all")]
        public ActionResult<string> GetProduct()
        {

            var resultProducts = ProductControllerFs.GetaAllProductsList(_context);

            if (FSharpOption<List<Product>>.get_IsSome(resultProducts))
                return Ok(resultProducts.Value);
            else
                return BadRequest();

            //try
            //{
            //    var productsList = await _context.Products.ToListAsync();
            //    return Ok(productsList);
            //}
            //catch (Exception exx)
            //{
            //    return BadRequest(exx);
            //}
        }

        [HttpGet("product/{id}")]
        //FsWork
        public ActionResult<string> GetProduct(int id)
        {
            var product = ProductControllerFs.GetProductFromId(_context, id);

            if (FSharpOption<Product>.get_IsSome(product))
                return Ok(product.Value);
            else
                return BadRequest();

            //try
            //{
            //    var product =  _context.Products.FirstOrDefault(p => p.Id == id);
            //    var images = _context.ProductImages.Where(x => x.Productid == product.Id).ToList();
            //    foreach (var image in images)
            //    {
            //        image.Image = _context.Images.FirstOrDefault(x => x.Id == image.Id);
            //    }

            //    product.MainpictureurlNavigation = _context.Images.Find(product.Mainpictureurl);
            //    product.ProductImages = images;

            //    var _images = _context.Images.ToList();
            //    var _images2 = _context.ProductImages.ToList();
            //    return Ok(product);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);
            //}
        }

        [HttpGet("product/{id}/products")]
        public ActionResult<string> GetPoductsWith(int id)
        
        {
            var allProductOrders = _context.OrderProducts.ToList();
            var productInOrder = _context.OrderProducts.Where(op => op.Productid == id).ToList();
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


        //FSharp work
        [HttpGet("category/{categoryId}")]
        public ActionResult<string> GetProductsFromCategory(int categoryId)
        {
            var getResult = ProductControllerFs.GetProductsFromCategory(_context, categoryId);

            if (FSharpOption<Microsoft.FSharp.Collections.FSharpList<Product>>.get_IsSome(getResult))
                return Ok(getResult.Value);
            else
                return BadRequest();

            //var selectedProducts =  _context.Products
            //    .Where(p => p.Category == categoryId)
            //    .ToList();

            //return JsonConvert.SerializeObject(selectedProducts);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("update/{id}")]
        //Fs work
        public IActionResult UpdateProduct(int id,[FromBody]Product modifiedProduct)
        {
            var updateResult = ProductControllerFs.UpdateProduct(_context, id, modifiedProduct);

            if (FSharpOption<int>.get_IsSome(updateResult))
                return Ok(updateResult.Value);
            else
                return BadRequest();


            //try
            //{
            //    var product = _context.Products.Find(id);

            //    if (modifiedProduct.Id != id || modifiedProduct.Category == null)
            //        throw new ArgumentException();

            //    product.Category = modifiedProduct.Category;
            //    product.CategoryNavigation = modifiedProduct.CategoryNavigation;
            //    product.Characteristics = modifiedProduct.Characteristics;
            //    product.Description = modifiedProduct.Description;
            //    product.Mainpictureurl = modifiedProduct.Mainpictureurl;
            //    product.MainpictureurlNavigation = modifiedProduct.MainpictureurlNavigation;
            //    product.Name = modifiedProduct.Name;
            //    product.OrderProducts = modifiedProduct.OrderProducts;
            //    product.Price = modifiedProduct.Price;
            //    product.Producer = modifiedProduct.Producer;
            //    product.ProductImages = modifiedProduct.ProductImages;
            //    product.ProductRating = modifiedProduct.ProductRating;
            //    product.Review = modifiedProduct.Review;              

            //    _context.SaveChanges();
            //    _context.Entry(modifiedProduct).State = EntityState.Modified;

            //    return Ok(modifiedProduct.Id);

            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex);
            //}
        }

        // POST: api/Products
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //Fs work
        [HttpPost("addProduct")]
        public ActionResult<string> AddProduct([FromBody]Product newProduct)
        {
            var option = FunctionLibraryFS.ProductControllerFs.AddProduct(_context,newProduct);

            if (FSharpOption<int>.get_IsSome(option))
                return Ok(option.Value);
            else
                return BadRequest();

            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        if (newProduct.Category == null)
            //            throw new ArgumentException();

            //        _context.Products.Add(newProduct);
            //        _context.SaveChanges();

            //        var newProductId = _context.Products.ToList().LastOrDefault();

            //        transaction.Commit();
            //        return Ok(newProductId);
            //    }
            //    catch (Exception ex)
            //    {
            //        transaction.Rollback();
            //        return BadRequest(ex);
            //    }
            //}
        }

        // DELETE: api/Products/5
        //FS work
        [HttpDelete("delete/{id}")]
        public  ActionResult<Product> DeleteProduct(int id)
        {
            var result = ProductControllerFs.DeleteProduct(_context, id);

            if (FSharpOption<int>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();



            //var product =  _context.Products.Find(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //_context.Products.Remove(product);
            // _context.SaveChanges();

            //return product;
        }
    }
}
