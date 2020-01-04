using DataClassLibrary.DbContext;
using NUnit.Framework;
using DataClassLibrary;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Net;
using Newtonsoft.Json;
using WebMarket;

namespace UnitTests
{
    public class Tests
    {
        private d6h4jeg5tcb9d8Context _context;

        [Test]
        public void ReviewTest()
        {
            _context = new d6h4jeg5tcb9d8Context();
            var productId = 4;

            var expectedReviewsList = _context.Reviews.Where(r => r.ProductId == productId).ToList();

            var result = new List<Review>();

            using (WebClient client = new WebClient())
            {
                var response = client.DownloadString("https://webmarket911.herokuapp.com/api/reviews/4");

                result = JsonConvert.DeserializeObject<List<Review>>(response);
            }

            Xunit.Assert.Equal(expectedReviewsList.Count(), result.Count());
        }
    }
}