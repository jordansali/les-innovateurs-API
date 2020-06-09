using JeopardyWebAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Data.EFCore
{
    public interface IJeopardyRepository
    {
        // General 
        Task<bool> SaveChangesAsync();

        Task<Questions[]> GetBoard();

        // Categories
        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <returns></returns>
        Task<Categories[]> GetAllCategories();

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Categories> GetCategoryById(int id);

        /// <summary>
        /// Add a category
        /// </summary>
        void AddCategory(Categories category);

        /// <summary>
        /// Delete a category
        /// </summary>
        void DeleteCategory(Categories category);

        Task<Categories> GetCategoryByCategoryNameEn(string nameEn);

        // Questions
        /// <summary>
        /// Get a list of all questions
        /// </summary>
        /// <returns></returns>
        Task<Questions[]> GetAllQuestions();

        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Questions> GetQuestionById(int id);

        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Questions[]> GetQuestionsByCategory(int id);

        /// <summary>
        /// Get a list of question by point value
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        Task<Questions[]> GetQuestionsByPoints(int points);

        /// <summary>
        /// Add a question
        /// </summary>
        void AddQuestion(Questions question);

        /// <summary>
        /// Delete a question
        /// </summary>
        void DeleteQuestion(Questions question);

}
}
