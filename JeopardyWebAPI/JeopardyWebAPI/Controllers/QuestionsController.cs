using AutoMapper;
using JeopardyWebAPI.Data.EFCore;
using JeopardyWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JeopardyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {


        private readonly IJeopardyRepository _repository;
        private readonly IMapper _mapper;

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

        [HttpGet("{points}")]
        public async Task<ActionResult<QuestionsModel>> GetByPoints(int points)
        {
            try
            {
                var result = await _repository.GetQuestionsByPoints(points);

                var mappedResult = _mapper.Map<IEnumerable<QuestionsModel>>(result);

                if(mappedResult == null)
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

                        //required to put question
                        if(model.QuestionEn == null)
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

               

                    #region properties check null
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
                    return Ok();
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