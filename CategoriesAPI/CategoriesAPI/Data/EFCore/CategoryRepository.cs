using CategoriesAPI.Models;

namespace CategoriesAPI.Data.EFCore
{
    public class CategoryRepository : CategoryBaseRepository<Categories,JeopardyDbContext>
    {
        public CategoryRepository(JeopardyDbContext context) : base(context)
        {
            
        }
    }
}
