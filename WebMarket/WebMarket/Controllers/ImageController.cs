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