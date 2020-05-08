using CategoriesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesAPI.DTO
{
    public class CategoryDTO
    {
        private AMCDbContext dbContext;

        public CategoryDTO(AMCDbContext databaseContext)
        {
            this.dbContext = databaseContext;
        }

        public int CategoryId { get; set; }

        [Required]
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }
    }
}
