﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JeopardyWebApp.Data.EFCore
{

    public abstract class QuestionBaseRepository<TEntity, TContext>  :
        IQuestionRepository<TEntity>
        where TEntity  : class, IQuestionEntity
        where TContext : DbContext
    {
        private readonly TContext context;

        public QuestionBaseRepository(TContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add a Question
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> AddQuestion(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }
        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteQuestion(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        /// <summary>
        /// Get a list of all categories
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllQuestions()
        {
            var entity = await context.Set<TEntity>().ToListAsync();
            return entity;

        }
        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> GetQuestionById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        /// <summary>
        /// Update an existing category
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<TEntity> UpdateQuestion(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Get question randomly
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetRandom()
        {
            ///TODO  - FIGURE OUT HOW TO MAKE IT RANDOM
            var entity = await context.Set<TEntity>().ToListAsync();

            Random rnd = new Random();
            int randomEntityNumber = rnd.Next(entity.Count);

            //var randomEntity = entity.IndexOf(randomEntityNumber);

            return randomEntity;

        }

        /// <summary>
        /// Get a list of question by point value
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetQuestionsByPoints(int points)
        {
            //TODO - FIGURE OUT HOW TO GET LIST OF QUESTIONS BY POINTS
            var entity = await context.Set<TEntity>().ToListAsync();
            return entity;
        }

    }

  
}