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
    public class CategoriesController : ControllerBase
    {

            private readonly IJeopardyRepository _repository;
            private readonly IMapper _mapper;

            public CategoriesController(IJeopardyRepository repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

/*
            [HttpGet]
            public async Task<ActionResult<CategoriesModel>> Get()
            {
                try
                {
                    var result = await _repository.GetAllCategories();
                    var mappedResult = _mapper.Map<IEnumerable<CategoriesModel>>(result);

                //if no categories exist in the database
                if (mappedResult == null)
                {
                    return NotFound("Categories not found");
                }
                return Ok(mappedResult);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
            } */

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesModel>> GetById(int id)
        {
            try
            {
                var result = await _repository.GetCategoryById(id);
                var mappedResult = _mapper.Map<CategoriesModel>(result);

                //if id entered contains string
            

                //if category object does not exist, return this error
                if (mappedResult == null)
                {
                    return NotFound("Category does not exist");
                }

                return Ok(mappedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        public int[] GetRandomFiveCategories()
        {

            var test = _repository.RandomizeFiveCategories();

           var id = 0;

            return test;


        }


        [HttpPost]
            public async Task<ActionResult<CategoriesModel>> Post(CategoriesModel model)
            {
                try
                {
                //Make sure to return an error if an existing Category Name is entered
                    if (await _repository.GetCategoryByCategoryNameEn(model.CategoryNameEn) != null)
                    {
                    StatusCode(StatusCodes.Status409Conflict, "The category name already exists");
                }
                if (await _repository.GetCategoryByCategoryNameEn(model.CategoryNameEn) == null)
                {
                    BadRequest("Category Name cannot be empty");
                }
                //Make sure to return error if an existing id is entered
                if (await _repository.GetCategoryById(model.Id) != null)
                {
                
                    StatusCode(StatusCodes.Status409Conflict, "This category already exists");
                }
                //Return an error if user tries to create without an id
                if (await _repository.GetCategoryById(model.Id) == null)
                {
                    BadRequest("Id must be entered.");
                }

                if (ModelState.IsValid)
                    {
                        var cat = _mapper.Map<Categories>(model);

                        _repository.AddCategory(cat);

                        if (await _repository.SaveChangesAsync())
                        {
                            var newModel = _mapper.Map<CategoriesModel>(cat);

                            return CreatedAtRoute("", new { cat = newModel.CategoryNameEn }, newModel);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex);
                }
                return BadRequest(ModelState);

            }

            [HttpPut("{id:int}")]
            public async Task<ActionResult<CategoriesModel>> Put(int id, CategoriesModel catModel)
            {
                try
                {
                    var cat = await _repository.GetCategoryById(id);
                    if (cat == null) return NotFound();

                #region check properties null

                if (catModel.CategoryNameEn != null)
                {
                    cat.CategoryNameEn = catModel.CategoryNameEn;
                }
                if (catModel.CategoryNameFr != null)
                {
                    cat.CategoryNameFr = catModel.CategoryNameFr;
                }
           

                #endregion

                //    _mapper.Map(catModel, cat);

                if (await _repository.SaveChangesAsync())
                    {
                        return Ok(_mapper.Map<CategoriesModel>(cat));
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

            [HttpDelete]
            public async Task<ActionResult<CategoriesModel>> Delete(int id, CategoriesModel model)
            {
                try
                {
                    var cat = await _repository.GetCategoryById(id);
                    if (cat == null) return NotFound();

                //prevent deletion of a question that has a category attached
                if (model.Questions != null)
                {
                    return BadRequest("You can't delete a Question related to a Category");
                }

                _repository.DeleteCategory(cat);

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

