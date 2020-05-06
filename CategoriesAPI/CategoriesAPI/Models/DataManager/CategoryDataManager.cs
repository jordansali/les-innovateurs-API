using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoriesAPI.Models.DataManager;
using CategoriesAPI.Models.Repository;
using CategoriesAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;


namespace CategoriesAPI.Models.DataManager
{
    public class CategoryDataManager : IDataRepository<Categories, CategoryDTO>
    {
        readonly AMCDbContext _catContext;

        public CategoryDataManager(AMCDbContext categoryContext)
        {
            _catContext = categoryContext;
        }

        public IEnumerable<Categories> GetAll()
        {
            return _catContext.Categories
            //    .Include(categories => categories.)
                .ToList();
        }

        public Categories Get(long id)
        {
            var author = _catContext.Categories
                .SingleOrDefault(b => b.CategoryId == id);

            return author;
        }

        public CategoryDTO GetDto(long id)
        {
            _catContext.ChangeTracker.LazyLoadingEnabled = true;

            using (var context = new AMCDbContext())
            {
                var author = context.Categories
                    .SingleOrDefault(b => b.CategoryId == id);

                return CategoriesDTOMapper.MapToDto(author);
            }
        }


        public void Add(Categories entity)
        {
            _catContext.Categories.Add(entity);
            _catContext.SaveChanges();
        }

        public void Update(Categories entityToUpdate, Categories entity)
        {
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

        public void Delete(Categories entity)
        {
            entity = _catContext.Categories
                .SingleOrDefault(b => b.CategoryId == entity.CategoryId);

            _catContext.Categories.Remove(entity);
           // throw new System.NotImplementedException();
        }
    }
}
