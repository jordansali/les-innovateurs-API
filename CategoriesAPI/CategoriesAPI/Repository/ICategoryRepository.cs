using CategoriesAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoriesAPI.Models;

namespace CategoriesAPI.Repository
{
    /// <summary>
    /// Interface for Category Repository
    /// </summary>
    public interface ICategoryRepository<Categories, C>
    {    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Categories> GetAllCategories();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Categories GetCategoryById(int id);        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void AddCategory(CategoryDTO entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        void UpdateCategory(Categories entity, int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void DeleteCategory(Categories entity);
        
    }
}
