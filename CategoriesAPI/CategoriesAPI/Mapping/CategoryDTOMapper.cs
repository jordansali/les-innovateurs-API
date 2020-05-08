using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using CategoriesAPI.DTO;
using CategoriesAPI.Models;
using CategoriesAPI.ResultSet;

namespace CategoriesAPI.Mapping
{
    public class CategoryDTOMapper : Profile
    {
        private AMCDbContext _dbContext;
        
        public CategoryDTOMapper(AMCDbContext databaseContext)
        {
            _dbContext = databaseContext;
        }

        public CategoryDTO MapToDto(Categories categories)
        {

            CreateMap<CategoryResultSet, CategoryDTO>();

            return new CategoryDTO(_dbContext)
            {
                CategoryId = categories.CategoryId,
                CategoryNameEn = categories.CategoryNameEn,
                CategoryNameFr = categories.CategoryNameFr,
            };
        }
       
    }
}
