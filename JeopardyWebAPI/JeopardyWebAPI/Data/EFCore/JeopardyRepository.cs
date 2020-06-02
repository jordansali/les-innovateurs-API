using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Questions.Add(question);
        }

        public void DeleteQuestion(Questions question)
        {
            _context.Questions.Remove(question);
        }

        public async Task<Questions[]> GetAllQuestions()
        {
           //   IQueryable<Questions> query = _context.Questions;
            var query = await _context.Questions.Include(c => c.Category).ToArrayAsync();

            return query;
        }

        public async Task<Questions> GetQuestionByCategory(string nameEn, int questionId)
        {
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == questionId && q.Category.CategoryNameEn == nameEn);

            return await query.FirstOrDefaultAsync();

        }

        public async Task<Questions> GetQuestionById(int id)
        {
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
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Points == points);

            return await query.ToArrayAsync();
        }
    }
}
