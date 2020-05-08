using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using CategoriesAPI.DTO;
using CategoriesAPI.Models;
using CategoriesAPI.ResultSet;
using CategoriesAPI.Data.EFCore;

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
                Id = categories.Id,
                CategoryName_En = categories.CategoryName_En,
                CategoryName_Fr = categories.CategoryName_Fr,
            };
        }
       
    }
}
