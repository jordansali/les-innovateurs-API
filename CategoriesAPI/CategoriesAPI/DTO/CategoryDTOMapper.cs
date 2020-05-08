using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using CategoriesAPI.Models;

namespace CategoriesAPI.DTO
{
    public static class CategoryDTOMapper
    {
        public static CategoryDTO MapToDto(Categories categories)
        {
            return new CategoryDTO()
            {
                CategoryId = categories.CategoryId,
                CategoryNameEn = categories.CategoryNameEn,
                CategoryNameFr = categories.CategoryNameFr,
            };
        }
       
    }
}
