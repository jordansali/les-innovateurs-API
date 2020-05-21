using JeopardyWebApp.Models;

namespace JeopardyWebApp.Data.EFCore
{
    public class CategoryRepository : CategoryBaseRepository<Categories, JeopardyDbContext>
    {
        public CategoryRepository(JeopardyDbContext context) : base(context)
        {

        }
    }
}
