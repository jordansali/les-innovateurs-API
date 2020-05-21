using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebApp.Data
{
    public interface IQuestionRepository<T> where T : class, IQuestionEntity
    {
        /// <summary>
        /// Get a list of all questions
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllQuestions();

        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetQuestionById(int id);

        /// <summary>
        /// Get question randomly
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetRandom();

        /// <summary>
        /// Get a list of question by point value
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        Task<List<T>> GetQuestionsByPoints(int points);


        /// <summary>
        /// Add a question
        /// </summary>
        /// <param name="entity"></param>
        Task<T> AddQuestion(T entity);

        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="entityToUpdate"></param>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        Task<T> UpdateQuestion(T entity);

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="entity"></param>
        Task<T> DeleteQuestion(int id);

    }
}
