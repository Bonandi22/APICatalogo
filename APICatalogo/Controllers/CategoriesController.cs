﻿using APICatalogo.Context;
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
        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            var categories = _context.Categories?.ToList();
            if (categories is null)
            {
                return NotFound("Category not found...");
            }
            return categories;
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public ActionResult<Category> Get(int id)
        {
            var categories = _context.Categories?.FirstOrDefault(p => p.CategoryId == id);
            if (categories is null)
            {
                return NotFound("Product not found...");
            }
            return categories;
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if (category is null)
                return BadRequest();

            _context.Categories?.Add(category);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCategory",
                new { id = category.CategoryId }, category);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories?.FirstOrDefault(p => p.CategoryId == id);
            //var produto = _context.Products.Find(id);

            if (category is null)
            {
                return NotFound("Category not Found...");
            }
            _context.Categories?.Remove(category);
            _context.SaveChanges();

            return Ok(category);
        }
    }
}