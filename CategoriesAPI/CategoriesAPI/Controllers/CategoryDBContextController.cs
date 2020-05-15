using CategoriesAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoriesAPI.Controllers
{
    public abstract class CategoryDBContextController<TEntity, TRepository> : ControllerBase where TEntity : class, ICategoryEntity
        where TRepository : ICategoryRepository<TEntity>
    {

        private readonly TRepository repository;

        public CategoryDBContextController(TRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // GET: api/[controller]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
           var categories = await repository.GetAllCategories();



            return Ok(categories);
        }
        /// <summary>
        /// Hey it's Shanique! Getting a category by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // GET: api/[controller]/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var category = await repository.GetCategoryById(id);
            
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, TEntity category)
        {
            if(id != category.Id)
            {
                return BadRequest();
            }
            await repository.UpdateCategory(category);
            return NoContent();
        }
        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/[controller]/5
        // POST: api/[controller]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Post(TEntity category)
        {
            await repository.AddCategory(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var category = await repository.DeleteCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
    }
}
