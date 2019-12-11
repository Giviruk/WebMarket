using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataClassLibrary;
using System.Net.Http;
using System.Net;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using DataClassLibrary.DbContext;

namespace WebMarket.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly AbstractDbContext _context;

        public SearchController(AbstractDbContext context)
        {
            _context = context;
        }

        // GET: api/Search
        [HttpPost]
        public async Task<ActionResult<string>> GetProduct([FromBody]string request)
        {
            var toSearch = await NormilizeRequest(request);
            var toSearchArr = toSearch.Split(' ').Select(w => w.Remove(w.Length-2,2));
            var allProducts = _context.Products.ToList();
            var dict = new Dictionary<int, int>();
            foreach(var product in allProducts)
            {
                int count = 0;
                var name = product.Name;
                foreach(var word in toSearchArr)
                {
                    if (name.Contains(word))
                        count++;
                }
                dict.Add(product.Id, count);
            }
            //var keys = dict.OrderBy(pair => pair.Value).ToDictionary(pair=>pair.Key).Keys.ToList();
            var keys = dict.Where(pair => pair.Value > 0).OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key).Keys.ToList();
            var result = new List<Product>();
            foreach (var product in allProducts)
            {
                if (keys.Contains(product.Id))
                    result.Add(product);
            }
            return JsonConvert.SerializeObject(result);
        }

        public static async Task<string> NormilizeRequest(string request)
        {
            var baseUrl = @"https://www.google.com/search?q=";
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(baseUrl + request + " yandex market");
            var htmlVersion = "";
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                htmlVersion = await response.Content.ReadAsStringAsync();
            }
            var doc = await new HtmlParser().ParseDocumentAsync(htmlVersion);
            var result = doc.QuerySelectorAll("div")
                .Where(elem => elem.ClassName == "BNeawe vvjwJb AP7Wnd")
                .Select(elem => elem.TextContent)
                .Select(text => string.Concat(text.TakeWhile(chr => chr.ToString() != char.ConvertFromUtf32(8212))))
                .Select(text => string.Concat(text.TakeWhile(chr => chr.ToString() != "-")).Trim())
                .FirstOrDefault();
            return result;
        }
    }
}
