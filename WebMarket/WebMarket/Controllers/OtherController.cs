using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WebMarket.Controllers
{
    [Route("api/Others")]
    [ApiController]
    public class OtherController : ControllerBase
    {
        private readonly d6h4jeg5tcb9d8Context _context;

        public OtherController(d6h4jeg5tcb9d8Context context)
        {
            _context = context;
        }

        // GET: api/Products1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
            return await _context.Product.ToListAsync();
        }

        // GET: api/Products1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet("Novelties")]
        public string GetNovelties()
        {
            var random = new Random();
            var selectedProductsList = new List<Product>();

            var idList = _context.Product
                .Select(p => p.Id)
                .ToList();
            

            for(int i = 0; i<3;i++)
            {
                var val = random.Next();
                int index = idList[val % (idList.Count)];
                idList.Remove(index);
                selectedProductsList.Add(_context.Product.FirstOrDefault(p => p.Id == index));
            }

            return JsonConvert.SerializeObject(selectedProductsList);

        }

        [HttpGet("Bestsellers")]
        public string GetBestsellers()
        {
            var result = _context.Product
                .Where(p => p.Producer == "Apple")
                .ToList();
            result.Add(_context.Product.FirstOrDefault(p => p.Producer != "Apple"));

            return JsonConvert.SerializeObject(result);
        }
    }
}
