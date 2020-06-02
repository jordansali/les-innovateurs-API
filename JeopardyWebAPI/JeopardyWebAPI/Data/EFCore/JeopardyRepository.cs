using JeopardyWebAPI.Data;
using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Data.EFCore
{
    public class JeopardyRepository : IJeopardyRepository
    {
        private readonly JeopardyDbContext _context;
        public JeopardyRepository(JeopardyDbContext context)
        {
            _context = context;
        }

        // General
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        // Categories
        public void AddCategory(Categories category)
        {
            _context.Categories.Add(category);
        }

        public void DeleteCategory(Categories category)
        {
            _context.Categories.Remove(category);
        }

        public async Task<Categories[]> GetAllCategories()
        {
            
           // IQueryable<Categories> query = _context.Categories;
           
            var query = await _context.Categories.Include(q => q.Questions).ToArrayAsync();

            return query;

           
        }

        public async Task<Categories> GetCategoryByCategoryNameEn(string nameEn)
        {
            IQueryable<Categories> query = _context.Categories;

            query = query.Where(c => c.CategoryNameEn == nameEn);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Categories> GetCategoryById(int id)
        {
            IQueryable<Categories> query = _context.Categories;

            query = query.Where(c => c.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        // Questions        
        public void AddQuestion(Questions question)
        {
            //throw new NotImplementedException();
            _context.Questions.Add(question);
        }

        public void DeleteQuestion(Questions question)
        {
            //throw new NotImplementedException();
            _context.Questions.Remove(question);
        }

        public async Task<Questions[]> GetAllQuestions()
        {
            //throw new NotImplementedException();

           //   IQueryable<Questions> query = _context.Questions;
           //Questions.attach, questions.add, questions.include, questions.join
            var query = await _context.Questions.Include(c => c.Category).ToArrayAsync();

            return query;
        }

        public async Task<Questions> GetQuestionByCategory(string nameEn, int questionId)
        {
            //throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == questionId && q.Category.CategoryNameEn == nameEn);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Questions> GetQuestionById(int id)
        {
            //throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == id);

            Questions q = await query.FirstOrDefaultAsync();

            return q;
        }

        public async Task<Questions> GetRandomQuestion()
        {
            throw new System.NotImplementedException();

            // TODO
        }

        public async Task<Questions[]> GetQuestionsByPoints(int points)
        {
           // throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Points == points);

            return await query.ToArrayAsync();
        }
    }
}
