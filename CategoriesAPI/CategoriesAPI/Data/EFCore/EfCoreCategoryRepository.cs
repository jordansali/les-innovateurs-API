using CategoriesAPI.Models;

namespace CategoriesAPI.Data.EFCore
{
    public class EfCoreCategoryRepository : EfCoreRepository<Categories, AMCDbContext>
    {
        public EfCoreCategoryRepository(AMCDbContext context) : base(context)
        {
            
        }
    }
}
