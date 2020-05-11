using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoriesAPI.Data
{
    /// <summary>
    /// Interface for Category Repository
    /// </summary>
    public interface ICategoryRepository<T> where T : class, ICategoryEntity
    {    
        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllCategories();

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetCategoryById(int id);        
        
        /// <summary>
        /// Add a category
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddCategory(T entity);

        /// <summary>
        /// Update a category
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        Task<T> UpdateCategory(T entity);

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="entity"></param>
        Task<T> DeleteCategory(int id);
        
    }
}
