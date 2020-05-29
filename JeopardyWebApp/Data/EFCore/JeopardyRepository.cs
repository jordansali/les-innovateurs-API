using JeopardyWebApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebApp.Data.EFCore
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
            return (await _context.SaveChangesAsync()) > 0 ; 
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
            IQueryable<Categories> query = _context.Categories;

            return await query.ToArrayAsync();
        }

        public async Task<Categories> GetCategoryByCategoryNameEn(string nameEn) 
        {
            IQueryable<Categories> query = _context.Categories;

            query = query.Where(c => c.CategoryName_En == nameEn);

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
            IQueryable<Questions> query = _context.Questions;

            return await query.ToArrayAsync();
        }

        public async Task<Questions> GetQuestionByCategory(string nameEn, int questionId) 
        {
            //throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == questionId && q.Category.CategoryName_En == nameEn);

            return await query.FirstOrDefaultAsync();
            
        }

        public async Task<Questions> GetQuestionById(int id) 
        {
            //throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == id);
           
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Questions> GetRandomQuestion() 
        { 
            throw new System.NotImplementedException(); 

            // TODO
        }

        public async Task<Questions[]> GetQuestionsByPoints(int points) 
        {
            throw new NotImplementedException();
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Points == points);

            return await query.ToArrayAsync();
        }

    }
}
