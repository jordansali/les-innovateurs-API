using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoriesAPI.Models;
using CategoriesAPI.Models.DTO;
using CategoriesAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CategoriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class CategoriesController : ControllerBase
    {
        private readonly IDataRepository<Categories, CategoryDTO> _dataRepository;
        private readonly ILogger<CategoriesController> _logger;

        //Categories controller constructor with logger and data repo
        public CategoriesController(IDataRepository<Categories, CategoryDTO> dataRepository, ILogger<CategoriesController> logger)
        {
            _dataRepository = dataRepository;
            _logger = logger;
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
        public IActionResult Get()
        {
            var categories = _dataRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var categories = _dataRepository.GetDto(id);

            if (categories == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(categories);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post(Categories categories)
        {
            if (categories is null)
            {
                return NotFound("No Categories found");
            }

            _dataRepository.Add(categories);
            return CreatedAtRoute(".../api/Categories", new { Id = categories.CategoryId }, null);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Put(int id, [FromBody] Categories categories)
        {
            if (categories == null)
            {
                return BadRequest("Author is null.");
            }

            var categoryToUpdate = _dataRepository.Get(id);
            if (categoryToUpdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _dataRepository.Update(categoryToUpdate, categories);
            return Ok();
        }
        //DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, Categories categories)
        {
            var category =  _dataRepository.Get(id);

    
                if (category == null)
                {
                    return NotFound("Database not found, please check request");
                }

                //_dataRepository.Delete(category);

                return Ok(category);
            
          

        }

    }
}
