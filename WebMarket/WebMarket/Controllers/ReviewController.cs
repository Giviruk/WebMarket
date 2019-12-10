using System;
using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;

namespace WebMarket.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly AbstractDbContext _context;
        public ReviewController(AbstractDbContext context)
        {
            this._context = context;
        }

        [HttpGet("{productId}")]
        public ActionResult<string> GetProductReviews(int productId)
        {
            var result = _context.Reviews.Where(r => r.ProductId == productId).ToList();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Review review)
        {
            try
            {       
               await  _context.Reviews.AddAsync(review);
               await  _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}