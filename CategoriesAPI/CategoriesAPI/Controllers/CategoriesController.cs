using CategoriesAPI.Data.EFCore;
using CategoriesAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace CategoriesAPI.Controllers
{
    
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class CategoriesController : CategoryDBContextController<Categories, CategoryRepository>
    {
       
        //CONSTRUCTOR
        public CategoriesController(CategoryRepository repository) : base(repository)
        {

        }
    }
}