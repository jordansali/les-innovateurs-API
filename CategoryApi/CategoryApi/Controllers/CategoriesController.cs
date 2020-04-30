using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CategoryApi.Models;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace CategoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CategoriesController : ControllerBase
    {

        private readonly ILogger<CategoriesController> _logger;
        private readonly AMCDbContext _context;
        
        
        //Add Logging console provider
       


        //Categories controller constructor with logger and context
        public CategoriesController(AMCDbContext context, ILogger<CategoriesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Categories
        ///<summary>
        ///Get list of all Category
        ///</summary>
        ///<returns>list of json </returns>
        ///<remarks>could put some sample request here  '\' is a line break to make the sample more readable</remarks>

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            var results = await _context.Category.ToListAsync();

            _logger.LogInformation("Get Category log test!");

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
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            try
            {
                if (category == null)
                {
                    return NotFound("Custom: Object not found, please check request");
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

        /*       [HttpGet("{title}")]
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
               }  */

        // PUT: api/Categories/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {

            if (id == null)
            {
                return NotFound("Item not found, please check request");
            }
            if (id != category.CategoryId)
            {
                return BadRequest("Not allowed or request not accepted");
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok();
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
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "The Api is experiencing technical difficulties");
            }

            // return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {

            try {
                _context.Category.Add(category);
                await _context.SaveChangesAsync();

                var new_category = CreatedAtAction("GetCategory", new { id = category.CategoryId }, category);

                return Created(".../api/categories/", new_category);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "The Api is experiencing technical difficulties");
            }
        } 

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);

            try {
                if (category == null)
                {
                    return NotFound("Database not found, please check request");
                }

                _context.Category.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(category);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "The Api is experiencing technical difficulties");
            }
            
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
