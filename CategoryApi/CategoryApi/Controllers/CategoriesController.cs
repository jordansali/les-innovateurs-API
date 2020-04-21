using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryApi.Models;
using System.Linq.Expressions;

namespace CategoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AMCDbContext _context;

        public CategoriesController(AMCDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var results = await _context.Category.ToListAsync();

            try {
                if (results == null)
                {
                    return NotFound("Object not found, please check request");
                }

                return Ok(results);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "The Api is experiencing technical difficulties");
            }
            
        }

        // GET: api/Categories/5
        [HttpGet("{id: int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            try
            {
                if (category == null)
                {
                    return NotFound("Object not found, please check request");
                }

                if (id != category.CategoryId)
                {
                    return BadRequest();
                }

                return Ok(category);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "The Api is experiencing technical difficulties");
            }
           
        }

        [HttpGet("{title}")]
        public async Task<ActionResult<Category>> GetCategory(string title)
        {
            var category = await _context.Category.FindAsync(title);

            if (category == null)
            {
                return NotFound("Object not found, please check request");
            }

            if (title != category.TitleEn)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest("Not allowed or request not accepted");
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound("Item not found, please check request");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            var new_category = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

            //*******needs string uri*********
            return Created("..", new_category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound("Database not found, please check request");
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
