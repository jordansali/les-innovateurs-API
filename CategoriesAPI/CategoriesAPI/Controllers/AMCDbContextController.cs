using CategoriesAPI.Data;
using CategoriesAPI.DTO;
using CategoriesAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesAPI.Controllers
{
    public abstract class AMCDbContextController<TEntity, TRepository> : ControllerBase where TEntity : class, IEntity
        where TRepository : ICategoryRepository<TEntity>
    {

        private readonly TRepository repository;

        public AMCDbContextController(TRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            return await repository.GetAllCategories();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var category = await repository.GetCategoryById(id);
            
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity category)
        {
            if(id != category.Id)
            {
                return BadRequest();
            }
            await repository.UpdateCategory(category);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity category)
        {
            await repository.AddCategory(category);
            return CreatedAtAction("Get", new { id = category.Id }, category);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var category = await repository.DeleteCategory(id);
            if(category == null)
            {
                return NotFound();
            }
            return category;
        }
    }
}
