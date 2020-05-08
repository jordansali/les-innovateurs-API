using CategoriesAPI.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CategoriesAPI.DTO;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CategoriesAPI.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoryRepository : DbContext, ICategoryRepository<Categories, int>
    {
        #region Private Member Variables
        private AMCDbContext _dbContext;
        private IMapper _mapper;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="databaseContext"></param>
        /// <param name="dbcontext"></param>
        /// <param name="mapper"></param>
        public CategoryRepository(DbContextOptions databaseContext, AMCDbContext dbcontext, IMapper mapper) :base(databaseContext)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        // INHERITED METHODS BELOW
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void AddCategory(Categories entity)
        {
            _dbContext.Set<Categories>().Add(entity);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCategory(Categories entity)
        {
            _dbContext.Set<Categories>().Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categories> GetAllCategories()
        {
            return _dbContext.Categories.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void UpdateCategory(Categories entity, int id)
        {
            _dbContext.Set<Categories>().Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Categories ICategoryRepository<Categories, int>.GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }
    } 
}

    
        
            
    
    


