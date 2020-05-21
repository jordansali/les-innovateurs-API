using JeopardyWebApp.Data.EFCore;
using JeopardyWebApp.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace JeopardyWebApp.Controllers
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