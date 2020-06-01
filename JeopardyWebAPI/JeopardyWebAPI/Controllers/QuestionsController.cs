using AutoMapper;
using JeopardyWebAPI.Data;
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
                    var category = await _repository.GetCategoryById(model.Category.Id);
                    if (category != null)
                    {
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

                    _mapper.Map(model, question);

                    if (question.Category.Id != model.Category.Id)
                    {
                        var category = await _repository.GetCategoryById(model.Category.Id);
                        if (category != null) question.Category = category;
                    }

                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok(_mapper.Map<QuestionsModel>(model));
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