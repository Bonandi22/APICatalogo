using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
        {
            //return _context.Categories.Include(p=> p.Products).ToList();
            try
            {
                return _context.Categories!.Include(p => p.Products).Where(c => c.CategoryId <= 10).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "There was a problem processing your request.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {          
            try
            {
              return _context.Categories!.AsNoTracking().ToList();
            }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request."); }            

            
        }


        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            try
            {
                var categories = _context.Categories?.FirstOrDefault(p => p.CategoryId == id);
                if (categories is null)
                {
                    return NotFound("Category not found...");
                }
                return categories;
            }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request."); }

        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            try
            {
                if (category is null)
                    return BadRequest();

                _context.Categories?.Add(category);
                _context.SaveChanges();

                return new CreatedAtRouteResult("GetCategory",
                    new { id = category.CategoryId }, category);
            }
            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request."); }


        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            try
            {
                if (id != category.CategoryId)
                {
                    return BadRequest();
                }

                _context.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok(category);
            }

            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request."); }

        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var category = _context.Categories?.FirstOrDefault(p => p.CategoryId == id);

                if (category is null)
                {
                    return NotFound("Category not Found...");
                }
                _context.Categories?.Remove(category);
                _context.SaveChanges();

                return Ok(category);
            }

            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem processing your request."); }

        }
    }
}
