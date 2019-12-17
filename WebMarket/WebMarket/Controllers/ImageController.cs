using System;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;
using System.Collections.Generic;
using Microsoft.FSharp.Core;

namespace WebMarket.Controllers
{
    [Route("api/image")]
    public class ImageController : Controller
    {
        private readonly AbstractDbContext _context;
        public ImageController(AbstractDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Images.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var result = _context.Images
                    .FirstOrDefault(c => c.Id == id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("add")]
        public IActionResult Put([FromBody]Image image)
        {
            try
            {
                _context.Images.Add(image);
                _context.SaveChanges();
                var images = _context.Images.ToList();
                var _imageId = images.Select(i => i.Id).ToList().Max();
                return Ok(_imageId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("deleteImage/{imageId}")]
        public IActionResult Delete(int imageId, [FromBody]int productId)
        {
            var result = FunctionLibraryFS.ImageControllerFs.DeleteImage(_context, imageId, productId);

            if (FSharpOption<Unit>.get_IsSome(result))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut("addProductImages/{productId}")]
        public IActionResult AddProductImages(int productId,[FromBody]List<Image> images)
        {

            var result = FunctionLibraryFS.ImageControllerFs.AddProductImages(_context, images, productId);

            if (FSharpOption<Unit>.get_IsSome(result))
                return Ok();
            else
                return BadRequest();

            //using (var transaction = _context.Database.BeginTransaction())
            //{
            //    try
            //    {
            //        var imagesIds = new List<int>();

            //        foreach(var i in images)
            //        {
            //            _context.Images.Add(i);
            //            _context.SaveChanges();
            //            var lastId = _context.Images.ToList().LastOrDefault().Id;

            //            imagesIds.Add(lastId);

            //        }


            //        foreach(var id in imagesIds)
            //        {
            //            _context.ProductImages.Add(new ProductImage() { Productid = productId, Imageid = id });
            //            _context.SaveChanges();
            //        }

            //        transaction.Commit();

            //        return Ok();
            //    }
            //    catch(Exception ex)
            //    {
            //        transaction.Rollback();
            //        return BadRequest(ex);
            //    }
            //}
        }

        [HttpPut("updateProducImages/{productId}")]
        public IActionResult UpdateProductImages(int productId, [FromBody]List<Image> images)
        {
            var result = FunctionLibraryFS.ImageControllerFs.UpdateProductImages(_context, images, productId);

            if (FSharpOption<Unit>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
        }
    }
}