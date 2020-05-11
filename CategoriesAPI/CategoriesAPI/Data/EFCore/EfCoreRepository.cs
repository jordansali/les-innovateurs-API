using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CategoriesAPI.Data.EFCore
{
    public abstract class EfCoreRepository<TEntity, TContext> : ICategoryRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : DbContext
    {
        private readonly TContext context;

        public EfCoreRepository(TContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> AddCategory(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

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

        public async Task<List<TEntity>> GetAllCategories()
        {
            var entity = await context.Set<TEntity>().ToListAsync();

            return entity;
            //throw new NotImplementedException();
            //TODO
            // DONT FORGET TODO!!!!
        }

        public async Task<TEntity> GetCategoryById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> UpdateCategory(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
