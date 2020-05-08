using AutoMapper;
using CategoriesAPI.DTO;
using CategoriesAPI.ResultSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoriesAPI.Mapping
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<CategoryResultSet, CategoryDTO>();
        }
    }
}
