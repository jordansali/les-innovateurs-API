using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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

            query = query.Where(c => c.Id == id).Include(q => q.Questions);

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
           //IQueryable<Questions> query = _context.Questions;
            var query = await _context.Questions.Include(c => c.Category).ToArrayAsync();

            return query;
        }

        public async Task<Questions[]> GetQuestionsByCategory(int categoryId)
        {
            var query = _context.Questions.ToArray();
          //  Questions[] results = new Questions()[];
            int points = 100;

            for (int i=0; i<5; i++)
            {
               // results[i] = (Questions) query.Where(q => q.Points == points);
                points += 100;
            }


            //query = query.Where(q => q.Category.CategoryNameEn == nameEn);

            return query;

        }

        public async Task<Questions> GetQuestionById(int id)
        {
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == id);

            Questions q = await query.FirstOrDefaultAsync();

            return q;
        }
        //used to populate the game board
        public async Task<Questions[]> GetQuestionsByPoints(int points)
        {
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Points == points).Include(c => c.Category);

            return await query.ToArrayAsync();
        }


      //  Task<Questions[]> GetBoard(){
        


      //  }


        public int[] RandomizeFiveCategories()
        {
            var query = _context.Categories.ToArray();
            int length = query.Length;

           // var rnd = new Random();
         //   query = query.OrderBy(x => rnd.Next()).Take(5);


           // return query;

            var random = new Random();
            int [] result = new int[5];

            int i = 0;
            while(i<5)
            {
                result[i] = query[random.Next(0, length)].Id;
                if (CheckforDuplicates(result))
                {
                    break;
                }
                else
                {
                    i++;
                }
                
            }                          

            return result; 

        }

        public bool CheckforDuplicates(int[] array)
        {
            var duplicates = array
             .GroupBy(p => p)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);


            return (duplicates.Count() > 0);



        }
    }
}
