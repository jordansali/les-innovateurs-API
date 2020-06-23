using AutoMapper;
using JeopardyWebAPI.Data.EFCore;
using JeopardyWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        #region Member variables
        private readonly IJeopardyRepository _repository;
        private readonly IMapper _mapper;
        #endregion

        public QuestionsController(IJeopardyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<QuestionsModel>> Get()
        {
            try
            {
                var result = await _repository.GetAllQuestions();
                var mappedResult = _mapper.Map<IEnumerable<QuestionsModel>>(result);

                if (mappedResult == null)
                {
                    return NotFound("No questions found");
                }

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("board")]
        public async Task<ActionResult<QuestionsModel>> GetGameBoard()
        {
            try
            {
                var result = await _repository.GetBoard();
                var mappedResult = _mapper.Map<IEnumerable<QuestionsModel>>(result);

                if (mappedResult == null)
                {
                    return NotFound("No questions found");
                }

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{points}")]
        public async Task<ActionResult<QuestionsModel>> GetByPoints(int points)
        {
            try
            {
                var result = await _repository.GetQuestionsByPoints(points);

                if (result.Length > 0)
                {
                    var mappedResult = _mapper.Map<IEnumerable<QuestionsModel>>(result);

                    if (mappedResult == null)
                    {
                        return NotFound("No questions found");
                    }

                    //BAD REQUEST Note: Not sure if works or not
                    if (string.IsNullOrEmpty(points.ToString()) || points == 0)
                    {
                        return BadRequest();
                    }

                    return Ok(mappedResult);
                }
                else
                {
                    return BadRequest("Bad request, value not found or does not exist.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
       
        [HttpPost]
        public async Task<ActionResult<QuestionsModel>> Post(QuestionsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // get the question's category
                    var category = await _repository.GetCategoryById(model.CategoryId);
                    if (category != null)
                    {
                        var q = await _repository.GetQuestionById(model.Id);
                        if (q == null)
                        {
                            //required to put question
                            if (model.QuestionEn == null)
                            {
                                return BadRequest("Question English cannot be null");
                            }

                            var question = _mapper.Map<Questions>(model);
                            question.Category = category;

                            _repository.AddQuestion(question);

                            if (await _repository.SaveChangesAsync())
                            {
                                return CreatedAtRoute("",
                                    new { category = category, id = question.Id },
                                    _mapper.Map<QuestionsModel>(question));
                            }
                        }
                        else {
                            return StatusCode(StatusCodes.Status409Conflict, "The question already exists");

                        }
                    }              
                    else {
                        return BadRequest("Category doesn't exist");
                    
                    }
                }
            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<QuestionsModel>> Put(int id, QuestionsModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var question = await _repository.GetQuestionById(id);
                    if (question == null) return NotFound();
               
                    #region Null check for Questions properties
                    if (model.QuestionEn != null)
                    {
                        question.QuestionEn = model.QuestionEn;
                    }
                    if (model.QuestionFr != null)
                    {
                        question.QuestionFr = model.QuestionFr;
                    }
                    if (model.AnswerEn != null) {
                        question.AnswerEn = model.AnswerEn;
                    }
                    if (model.AnswerFr != null)
                    {
                        question.AnswerFr = model.AnswerFr;
                    }
                    if (model.CategoryId != 0)
                    {
                        question.CategoryId = model.CategoryId;
                    }
                    if (model.Points != 0 && model.Points != null)
                    {
                        question.Points = model.Points;
                    }
                    if (model.TimeLimit != 0 && model.TimeLimit != null)
                    {
                        question.TimeLimit = model.TimeLimit;
                    }
                    if (model.Hint != null)
                    {
                        question.Hint = model.Hint;
                    }

                    #endregion

                    if (question.CategoryId != model.CategoryId)
                    {
                        var category = await _repository.GetCategoryById(model.CategoryId);
                        if (category != null) question.Category = category;
                    }

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(_mapper.Map<QuestionsModel>(question));
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<ActionResult<QuestionsModel>> Delete(int id)
        {
            try
            {
                var question = await _repository.GetQuestionById(id);
                if (question == null) return NotFound();                

                _repository.DeleteQuestion(question);

                if (await _repository.SaveChangesAsync())
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}