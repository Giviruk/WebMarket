using System;
using System.Linq;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;

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
            catch(Exception ex)
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
        
        [HttpPut]
        public IActionResult Put([FromBody]Image image)
        {
            try
            {
                _context.Images.Add(image);
                _context.SaveChanges();
                var images = _context.Images.ToList();
                var _image = images[images.Count - 1];
                return Ok(_image.Id);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _context.Images.Remove(_context.Images.Find(id));
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}