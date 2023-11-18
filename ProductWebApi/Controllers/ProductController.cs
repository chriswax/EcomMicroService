using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Models;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _context;

        public ProductController(ProductDbContext productDbContext)
        {
            this._context = productDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return _context.products;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetProductById(int productId)
        {
            var product = await _context.products.FindAsync(productId);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Create(Product product)
        {
            await _context.products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }

        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product)
        {
             _context.products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _context.products.FindAsync(productId);
              _context.products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok(product);

        }
    }
}
