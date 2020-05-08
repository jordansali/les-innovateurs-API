using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoriesAPI.Repository;
using CategoriesAPI.DTO;
using Microsoft.EntityFrameworkCore;
using CategoriesAPI.Mapping;

namespace CategoriesAPI.Models.DataManager
{
    /// <summary>
    /// CategoryDataManager class
    /// </summary>
    public class CategoryDataManager : ICategoryRepository<Categories,int>
    {
        readonly AMCDbContext _catContext;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="categoryContext"></param>
        public CategoryDataManager(AMCDbContext categoryContext)
        {
            _catContext = categoryContext;
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Categories> GetAllCategories()
        {

            return _catContext.Categories
            //    .Include(categories => categories.)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Categories GetCategoryById(int id)
        {
            var cat = _catContext.Categories
                .SingleOrDefault(b => b.CategoryId == id);

            return cat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void AddCategory(Categories entity)
        {
            _catContext.Categories.Add(entity);
            _catContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        public void UpdateCategory(Categories entity, int id)
        {
            /*
            entityToUpdate = _catContext.Categories
            //    .Include(a => a.CategoryNameEn)
            //    .Include(a => a.CategoryNameFr)
                .Single(b => b.CategoryId == entityToUpdate.CategoryId);

            entityToUpdate.CategoryNameEn = entity.CategoryNameEn;
            entityToUpdate.CategoryNameFr = entity.CategoryNameFr;

            // entityToUpdate.AuthorContact.Address = entity.AuthorContact.Address;
            // entityToUpdate.AuthorContact.ContactNumber = entity.AuthorContact.ContactNumber;

            /* var deletedBooks = entityToUpdate.BookAuthors.Except(entity.BookAuthors).ToList();
              var addedBooks = entity.BookAuthors.Except(entityToUpdate.BookAuthors).ToList();

              deletedBooks.ForEach(bookToDelete =>
                  entityToUpdate.BookAuthors.Remove(
                      entityToUpdate.BookAuthors
                          .First(b => b.BookId == bookToDelete.BookId)));

              foreach (var addedBook in addedBooks)
              {
                  _bookStoreContext.Entry(addedBook).State = EntityState.Added;
              } */

            _catContext.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteCategory(Categories entity)
        {
            entity = _catContext.Categories
                .SingleOrDefault(b => b.CategoryId == entity.CategoryId);

            _catContext.Categories.Remove(entity);
        }
    }
}
