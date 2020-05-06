using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesAPI.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryNameEn { get; set; }
        public string CategoryNameFr { get; set; }
    }
}
