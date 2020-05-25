using JeopardyWebApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JeopardyWebApp.Controllers
{

    public abstract class QuestionsDBContextController<TEntity, TRepository> : ControllerBase where TEntity : class, IQuestionEntity where TRepository : IQuestionRepository<TEntity>
    {
        private readonly TRepository repository;

        public QuestionsDBContextController(TRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Get a list of all questions
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // GET: api/[controller]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
        {
            var questions = await repository.GetAllQuestions();



            return Ok(questions);
        }
        /// <summary>
        /// Get question by id
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // GET: api/[controller]/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            var question = await repository.GetQuestionById(id);

            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(int id, TEntity question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            await repository.UpdateQuestion(question);
            return NoContent();
        }
        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // PUT: api/[controller]/5
        // POST: api/[controller]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Post(TEntity question)
        {
            await repository.AddQuestion(question);
            return CreatedAtAction("Get", new { id = question.Id }, question);
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var question = await repository.DeleteQuestion(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }


        /// <summary>
        ///Get a random question
        /// </summary>

        /// <returns></returns>
        // GetRandom: api/[controller]/5
        [HttpGet("bool")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get(bool rand = false)
        {
            var question = await repository.GetRandom();
            if (question.Count == 0)
            {
                return NotFound();
            }
            return Ok(question);
        }

        /// <summary>
        ///Get questions by points
        /// </summary>

        /// <returns></returns>
        // GetRandom: api/[controller]/5
        [HttpGet("point:int/bool")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get(int points,bool difficulty = true)
        {
            var question = await repository.GetQuestionsByPoints(points);
            if (question.Count == 0)
            {
                return NotFound();
            }
            return Ok(question);
        }
    }

}
