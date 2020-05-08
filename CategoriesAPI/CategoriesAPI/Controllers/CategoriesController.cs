using CategoriesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CategoriesAPI.Data.EFCore;

namespace CategoriesAPI.Controllers
{
    /// <summary>
    /// Categories Controller class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]    
    public class CategoriesController : AMCDbContextController<Categories, EfCoreCategoryRepository>
    {
        #region Private Member Variables
        private readonly AMCDbContext _dbContext;
           
        private readonly ILogger<CategoriesController> _logger;
        #endregion

        // CONSTRUCTOR
        public CategoriesController(EfCoreCategoryRepository repository) : base(repository)
        {

        }

        /*
        public CategoriesController(ILogger<CategoriesController> logger)
        {                                 
            _logger = logger;
        }
        */
        
        //// GET: api/Categories
        /////<summary>
        /////Get list of all Categories
        /////</summary>
        /////<returns>list of json </returns>
        /////<remarks>could put some sample request here  '\' is a line break to make the sample more readable</remarks>
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<IEnumerable<Categories>>> GetCategories()
        //{
        //    return await _dbContext.Categories.ToListAsync();
        //    /*
        //    var categories = _categoryRepository.GetAllCategories();
        //    return Ok(categories);
        //    */
        //}

        //// GET BY ID: api/Categories/{id}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult Get(int id)
        //{
        //    throw new NotImplementedException();
        //    /*
        //    var categories = _categoryRepository.GetCategoryById(id);

        //    if (categories == null)
        //    {
        //        return NotFound("Category not found.");
        //    }

        //    return Ok(categories);
        //    */
        //}
        
        //// POST
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="categories"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<CategoryDTO>> Post(CategoryDTO categoryDto)
        //{
        //    throw new NotImplementedException();
        //    /*
        //    var cat = new Categories
        //    {
        //        CategoryNameEn = categoryDto.CategoryNameEn,
        //        CategoryNameFr = categoryDto.CategoryNameFr
        //    };

        //    return CreatedAtRoute(".../api/Categories", new { Id = cat.CategoryId }, null);
            
        //    if (categoryDto is null)
        //    {
        //        return NotFound("No Categories found");
        //    }
        //    _categoryRepository.AddCategory(categoryDto);
        //    return CreatedAtRoute(".../api/Categories", new { Id = categoryDto.CategoryId }, null);
        //    */
        //}

        //// PUT
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="categories"></param>
        ///// <returns></returns>
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult Put(int id, [FromBody] Categories categories)
        //{
        //    throw new NotImplementedException();
        //    /*
        //    if (categories == null)
        //    {
        //        return BadRequest("Author is null.");
        //    }

        //    var categoryToUpdate = _categoryRepository.GetCategoryById(id);
        //    if (categoryToUpdate == null)
        //    {
        //        return NotFound("The Employee record couldn't be found.");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    _categoryRepository.UpdateCategory(categoryToUpdate,id);
        //    return Ok();
        //    */
        //}


        ////DELETE: api/Categories/5
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="categories"></param>
        ///// <returns></returns>
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Delete(int id, Categories categories)
        //{
        //    throw new NotImplementedException();
        //    /*
        //    var category =  _categoryRepository.GetCategoryById(id);

    
        //        if (category == null)
        //        {
        //            return NotFound("Database not found, please check request");
        //        }

        //        //_dataRepository.Delete(category);

        //        return Ok(category);
            
        //  */
        //}
   
    }
}
