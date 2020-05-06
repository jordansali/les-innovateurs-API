using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace CategoriesAPI.Models.DTO
{
    public static class CategoriesDTOMapper
    {
        public static CategoryDTO MapToDto(Categories categories)
        {
            return new CategoryDTO()
            {
                Id = categories.CategoryId,
                CategoryNameEn = categories.CategoryNameEn,
                CategoryNameFr = categories.CategoryNameFr,
            };
        }
       
    }
}
