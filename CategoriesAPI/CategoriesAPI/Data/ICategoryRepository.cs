using System.Collections.Generic;
using System.Threading.Tasks;
using CategoriesAPI.Data;

namespace CategoriesAPI.Data
{
    /// <summary>
    /// Interface for Category Repository
    /// </summary>
    public interface ICategoryRepository<T> where T : class, IEntity
    {    
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllCategories();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetCategoryById(int id);        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddCategory(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        Task<T> UpdateCategory(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        Task<T> DeleteCategory(int id);
        
    }
}
