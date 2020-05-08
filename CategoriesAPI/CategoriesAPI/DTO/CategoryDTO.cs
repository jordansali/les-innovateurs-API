using CategoriesAPI.Data.EFCore;
using System.ComponentModel.DataAnnotations;

namespace CategoriesAPI.DTO
{
    public class CategoryDTO
    {
        private AMCDbContext dbContext;

        public CategoryDTO(AMCDbContext databaseContext)
        {
            this.dbContext = databaseContext;
        }

        public int Id { get; set; }

        [Required]
        public string CategoryName_En { get; set; }
        public string CategoryName_Fr { get; set; }
    }
}
