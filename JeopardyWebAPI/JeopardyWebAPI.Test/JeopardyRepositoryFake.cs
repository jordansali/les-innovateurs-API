using JeopardyWebAPI.Data.EFCore;
using JeopardyWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Test
{
    public class JeopardyRepositoryFake : IJeopardyRepository
    {
        private List<Categories> _category;
        private List<Questions> _questions;
        private readonly JeopardyDbContext _context;
        readonly JeopardyRepositoryFake repositoryFake;


        public JeopardyRepositoryFake() {

                

        }

        private JeopardyDbContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<JeopardyDbContext>()
                     .UseInMemoryDatabase(Guid.NewGuid().ToString())
                     .Options;
            var context = new JeopardyDbContext(options);

            _category = new List<Categories>()
            {
                new Categories() {Id = 30, CategoryNameEn = "TestCat 1", CategoryNameFr = "TestCat 1 en francais"},
                new Categories() {Id = 31, CategoryNameEn = "TestCat 2", CategoryNameFr = "TestCat 2 en francais"},
                new Categories() {Id = 32, CategoryNameEn = "TestCat 3", CategoryNameFr = "TestCat 3 en francais"},
                new Categories() {Id = 33, CategoryNameEn = "TestCat 4", CategoryNameFr = "TestCat 4 en francais"},
                new Categories() {Id = 34, CategoryNameEn = "TestCat 5", CategoryNameFr = "TestCat 5 en francais"},
            };

            _questions = new List<Questions>()
            {
                new Questions() {Id = 30, QuestionEn = "TestQuestion 1", QuestionFr = "TestQuestion 1 en francais", AnswerEn = "TestAnswer 1", AnswerFr = "TestAnswer 1 en francais", CategoryId = 12, Hint = "Test Hint 1", Points = 100, TimeLimit = 30},
                new Questions() {Id = 31, QuestionEn = "TestQuestion 2", QuestionFr = "TestQuestion 2 en francais", AnswerEn = "TestAnswer 2", AnswerFr = "TestAnswer 2 en francais", CategoryId = 13, Hint = "Test Hint 2", Points = 200, TimeLimit = 30},
                new Questions() {Id = 32, QuestionEn = "TestQuestion 3", QuestionFr = "TestQuestion 3 en francais", AnswerEn = "TestAnswer 3", AnswerFr = "TestAnswer 3 en francais", CategoryId = 14, Hint = "Test Hint 3", Points = 300, TimeLimit = 30},
                new Questions() {Id = 33, QuestionEn = "TestQuestion 4", QuestionFr = "TestQuestion 4 en francais", AnswerEn = "TestAnswer 4", AnswerFr = "TestAnswer 4 en francais", CategoryId = 15, Hint = "Test Hint 4", Points = 400, TimeLimit = 30},
                new Questions() {Id = 34, QuestionEn = "TestQuestion 5", QuestionFr = "TestQuestion 5 en francais", AnswerEn = "TestAnswer 5", AnswerFr = "TestAnswer 5 en francais", CategoryId = 16, Hint = "Test Hint 5", Points = 500, TimeLimit = 30}
            };

            return context;
        }


        public void AddCategory(Categories category)
        {

            _category.Add(category);
        }

        public void AddQuestion(Questions question)
        {
            _questions.Add(question);
        }

        public void DeleteCategory(Categories category)
        {

            _category.Remove(category);
        }

        public void DeleteQuestion(Questions question)
        {
            _questions.Remove(question);
        }

        public async Task<Questions[]> GetAllQuestions()
        {
            var query = _questions.ToArray();
            return  query;
        }

        public async Task<Questions[]> GetBoard()
        {
            //TO DO
            Questions[] board = new Questions[25];
            int[] categoryIds = GetRandomCategoryIds();

            // get random categories
            for (int i = 0; i < 5; i++)
            {
                Questions[] questions = await GetRandomQuestionsByCategory(categoryIds[i]);

                for (int j = 0; j < 5; j++)
                {
                    // get question using category id              
                    board[i * 5 + j] = questions[j];
                }
            }

            return board;
        }

        public async Task<Categories> GetCategoryByCategoryNameEn(string nameEn)
        {
            // throw new NotImplementedException();
            var query = _category;
            query = query.Where(c => c.CategoryNameEn == nameEn).ToList();
            var query2 = query.FirstOrDefault();

            return query2;
        }

        public async Task<Categories> GetCategoryById(int id)
        {
            var query = _category;

            query = query.Where(c => c.Id == id).ToList();

            return query.FirstOrDefault();
        }

        public async Task<Questions> GetQuestionById(int id)
        {
           var query = _questions;

           query = query.Where(q => q.Id == id).ToList();

            return query.FirstOrDefault();
        }

        public async Task<Questions[]> GetQuestionsByCategory(int id)
        {
            var query = _questions;

            query = query.Where(q => q.Category.Id == id).ToList();

            return query.ToArray();
        }

        public async Task<Questions[]> GetQuestionsByPoints(int points)
        {
            var query = _questions;

            query = query.Where(q => q.Points == points).ToList();

            return query.ToArray();
        }

       public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        } 

        public async Task<Categories[]> GetAllCategories()
        {
            var query =  _category.ToArray();
            return query;
        }

        public async Task<Questions[]> GetRandomQuestionsByCategory(int catId)
        {
            Questions[] questions = new Questions[5];

            for (int i = 0; i < 5; i++)
            { 
                var x = await GetQuestionByCategoryAndPoints(catId, (i + 1) * 100);
                questions[i] = x;
            }

            return questions;
        }
        public int[] GetRandomCategoryIds()
        {
            try
            {
                var rand = new Random();

                // get all the categories
                var query = _category.ToArray();

                // array to store ids
                int[] results = new int[5];

                for (int i = 0; i < 5; i++)
                {
                    // select one at random                
                    results[i] = query[rand.Next(query.Length)].Id;
                }

                return results;
            }
            catch
            {
                return null;
            }
        }

        private async Task<Questions> GetQuestionByCategoryAndPoints(int categoryId, int points)
        {
            try
            {
                // get question using category id and points value             
                var query = _questions;

                query = query.Where(q => q.Category.Id == categoryId).ToList();
                var query2 = query.Where(q => q.Points == points).ToList();

               // await query.ToList();

                // randomize the results and get the first item 
                var results = query2.OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                return results;
            }
            catch
            {
                return null;
            }
        }

    }
}
