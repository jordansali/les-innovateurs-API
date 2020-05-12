using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoriesAPI.Data.EFCore
{
    public abstract class CategoryBaseRepository<TEntity, TContext> : ICategoryRepository<TEntity>
        where TEntity : class, ICategoryEntity
        where TContext : DbContext
    {
        private readonly TContext context;

        public CategoryBaseRepository(TContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Add a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddCategory(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteCategory(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if(entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllCategories()
        {
            var entity = await context.Set<TEntity>().ToListAsync();
            return entity;

        }
        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> GetCategoryById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateCategory(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
