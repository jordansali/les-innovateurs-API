using AutoMapper;
using JeopardyWebAPI.Data;
using JeopardyWebAPI.Data.EFCore;
using JeopardyWebAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
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
            }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriesModel>> GetById(int id)
        {
            try
            {
                var result = await _repository.GetCategoryById(id);
                var mappedResult = _mapper.Map<CategoriesModel>(result);

               // int something = 0;
                //if id entered does not exist, return error Bad Request "id does not exist"?

                //if id entered contains string
            /*    if (int.TryParse(id, out something))
                {
                    BadRequest("incorrect format for id");
                } */

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


        [HttpPost]
            public async Task<ActionResult<CategoriesModel>> Post(CategoriesModel model)
            {
                try
                {
                //Make sure to return an error if an existing Category Name is entered
                    if (await _repository.GetCategoryByCategoryNameEn(model.CategoryNameEn) != null)
                    {
                        ModelState.AddModelError("Id", "This name for this Category already exists.");
                    }
                //Make sure to return error if an existing id is entered
                if (await _repository.GetCategoryById(model.Id) != null)
                {
                    ModelState.AddModelError("Id", "This id for this category already exists.");
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
            public async Task<ActionResult<CategoriesModel>> Delete(int id)
            {
                try
                {
                    var cat = await _repository.GetCategoryById(id);
                    if (cat == null) return NotFound();

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

