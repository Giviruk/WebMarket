using System;
using System.Linq;
using System.Threading.Tasks;
using DataClassLibrary;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;
using Microsoft.FSharp.Core;
using System.Collections.Generic;

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
            var result = FunctionLibraryFS.ReviewControollerFs.GetProductReviews(_context, productId);

            if (FSharpOption<List<Review>>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Review review)
        {
            var result = FunctionLibraryFS.ReviewControollerFs.AddProduct(_context, review);

            if (FSharpOption<Unit>.get_IsSome(result))
                return Ok(result.Value);
            else
                return BadRequest();
            //try
            //{       
            //   await  _context.Reviews.AddAsync(review);
            //   await  _context.SaveChangesAsync();

            //    return Ok();
            //}
            //catch (Exception exception)
            //{
            //    return BadRequest(exception);
            //}
        }
    }
}