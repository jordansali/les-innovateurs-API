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
                    if (await _repository.GetCategoryByCategoryNameEn(model.CategoryNameEn) != null)
                    {
                        ModelState.AddModelError("Id", "Id in use.");
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

                    _mapper.Map(catModel, cat);

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

