using System.Linq;
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
                return Ok(resultProducts.Value.Where(x=>x.Status != "delete").ToList());
            else
                return BadRequest();
        }

        [HttpGet("product/{id}")]
        //FsWork
        public ActionResult<string> GetProduct(int id)
        {
            var product = ProductControllerFs.GetProductFromId(_context, id);

            if (product.Value.Status != "delete")
            {
                if (FSharpOption<Product>.get_IsSome(product))
                    return Ok(product.Value);
                else
                    return BadRequest();
            }
            else
                return BadRequest("Product was deleted");
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
            return JsonConvert.SerializeObject(result.Where(x=>x.Status != "delete").ToList());
        }


        //FSharp work
        [HttpGet("category/{categoryId}")]
        public ActionResult<string> GetProductsFromCategory(int categoryId)
        {
            //var getResult = ProductControllerFs.GetProductsFromCategory(_context, categoryId);

            //if (FSharpOption<Microsoft.FSharp.Collections.FSharpList<Product>>.get_IsSome(getResult))
            //    return Ok(getResult.Value);
            //else
            //    return BadRequest();
            var category = _context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == categoryId);
            return Ok(category.Products);
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
        }
    }
}
