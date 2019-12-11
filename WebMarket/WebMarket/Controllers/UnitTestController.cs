using Microsoft.AspNetCore.Mvc;
using DataClassLibrary.DbContext;
using Xunit;
using System.Linq;
using Newtonsoft.Json;
using DataClassLibrary;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;

namespace WebMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitTestController : ControllerBase
    {
        private readonly AbstractDbContext _context;

        public UnitTestController(AbstractDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult TestGetProductReiew()
        {
            ReviewController controller = new ReviewController(_context);

            var productId = 4;

            var expectedReviewsList = _context.Reviews.Where(r => r.ProductId == productId).ToList();

            var result = new List<Review>();

            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://localhost:44369/api/reviews/4");

                result = JsonConvert.DeserializeObject<List<Review>>(response);
            }

            try
            {
                Assert.Equal(expectedReviewsList.FirstOrDefault().ReviewId, result.FirstOrDefault().ReviewId);
                return Ok("true");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Fact]
        public void TestPostProductReview()
        {

        }

    }
}