using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var product = _context.Products?.ToList();
            if (product is null)
            {
                return NotFound("Product not found...");
            }
            return product;
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        public ActionResult<Product> Get(int id)
        {
            var produto = _context.Products?.FirstOrDefault(p => p.ProductId == id);
            if (produto is null)
            {
                return NotFound("Product not found...");
            }
            return produto;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null)
                return BadRequest();

            _context.Products?.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetProduct",
                new { id = product.ProductId }, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var product = _context.Products?.FirstOrDefault(p => p.ProductId == id);
            //var produto = _context.Products.Find(id);

            if (product is null)
            {
                return NotFound("Product not Found...");
            }
            _context.Products?.Remove(product);
            _context.SaveChanges();

            return Ok(product);
        }


    }
}
