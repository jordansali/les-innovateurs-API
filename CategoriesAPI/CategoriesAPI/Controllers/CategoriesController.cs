using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoriesAPI.Models;
using CategoriesAPI.DTO;
using CategoriesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CategoriesAPI.Controllers
{
    /// <summary>
    /// Categories Controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController : ControllerBase
    {        
        #region Private Member Variables
        private ICategoryRepository<Categories,int> _categoryRepository;     
        private readonly ILogger<CategoriesController> _logger;
        #endregion

        // CONSTRUCTOR
        public CategoriesController(ICategoryRepository<Categories,int> categoryRepository,  ILogger<CategoriesController> logger)
        {            
            _categoryRepository = categoryRepository;           
            _logger = logger;
        }

        // GET: api/Categories
        ///<summary>
        ///Get list of all Categories
        ///</summary>
        ///<returns>list of json </returns>
        ///<remarks>could put some sample request here  '\' is a line break to make the sample more readable</remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var categories = _categoryRepository.GetAllCategories();
            return Ok(categories);
        }

        // GET BY ID: api/Categories/{id}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            var categories = _categoryRepository.GetCategoryById(id);

            if (categories == null)
            {
                return NotFound("Category not found.");
            }

            return Ok(categories);
        }
        
        // POST
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        {
            var cat = new Categories
            {
                CategoryNameEn = categoryDto.CategoryNameEn,
                CategoryNameFr = categoryDto.CategoryNameFr
            };

            return Ok(cat);
                     
        }

        // PUT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
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

            var categoryToUpdate = _categoryRepository.GetCategoryById(id);
            if (categoryToUpdate == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _categoryRepository.UpdateCategory(categoryToUpdate,id);
            return Ok();
        }


        //DELETE: api/Categories/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id, Categories categories)
        {
            var category =  _categoryRepository.GetCategoryById(id);

    
                if (category == null)
                {
                    return NotFound("Database not found, please check request");
                }

                //_dataRepository.Delete(category);

                return Ok(category);
            
          

        }

    }
}
