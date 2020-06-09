using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
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
        public bool CheckforDuplicates(int[] array)
        {
            var duplicates = array
             .GroupBy(p => p)
             .Where(g => g.Count() > 1)
             .Select(g => g.Key);

            return (duplicates.Count() > 0);
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
            var query = await _context.Questions.Include(c => c.Category).ToArrayAsync();

            return query;
        }

        public async Task<Questions[]> GetQuestionsByCategory(int categoryId)
        {

            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Category.Id == categoryId);

            return await query.ToArrayAsync();

        }

        public async Task<Questions> GetQuestionById(int id)
        {
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Id == id);

            Questions q = await query.FirstOrDefaultAsync();

            return q;
        }
        
        public async Task<Questions[]> GetQuestionsByPoints(int points)
        {
            IQueryable<Questions> query = _context.Questions;

            query = query.Where(q => q.Points == points).Include(c => c.Category)//.ThenInclude(x => x.CategoryNameEn);

            return await query.ToArrayAsync();
        }


        //get 25 questions for the Jeopardy game board
        public async Task<Questions[]> GetBoard()
        {
            Questions[] board = new Questions[25];
            int[] categoryIds = GetRandomCategoryIds();            

            // get random categories
            for (int i = 0; i < 5; i++)
            {
                Questions[] questions = await GetRandomQuestionsByCategory(categoryIds[i]);

                for (int j = 0; j < 5; j++) {
                    // get question using category id              
                    board[i * 5 + j] = questions[j];
                }                                
            }
            return board;

        }

        public async Task<Questions[]> GetRandomQuestionsByCategory(int catId)
        {
            Questions[] questions = new Questions[5];
            
            for (int i = 0; i < 5; i++)
            {      
                //questions[i] = 
                var x = await GetQuestionByCategoryAndPoints(catId, (i + 1) * 100);
                questions[i] = x;
            }

            return questions;

        }

        private async Task<Questions> GetQuestionByCategoryAndPoints(int categoryId, int points)
        {
            try
            {
                Random rand = new Random();

                // get question using category id and points value             
                IQueryable<Questions> query = _context.Questions;

                query = query.Where(q => q.Category.Id == categoryId);
                query = query.Where(q => q.Points == points);

                // randomize the results and get the first item 
                var results = query.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                return results;
            }
            catch(Exception ex)
            {
                return null;
            }
        }


        public int[] GetRandomCategoryIds()
        {
            var rand = new Random();

            // get all the categories
            var query = _context.Categories.ToArray();

            // array to store ids
            int[] results = new int[5];

            for(int i = 0; i < 5; i++)
            {
                // select one at random                
                results[i] = query[rand.Next(query.Count())].Id;                

            }
            
            return results;

        }




        
    }
}
