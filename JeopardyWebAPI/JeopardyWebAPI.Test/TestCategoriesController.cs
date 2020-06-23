using JeopardyWebAPI.Controllers;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JeopardyWebAPI.Data.EFCore;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using AutoMapper;
using JeopardyWebAPI.Models;
using JeopardyWebAPI.Data;

namespace JeopardyWebAPI.Test
{
    [TestClass]
    public class TestCategoriesController
    {
        readonly CategoriesController _controller;
        private readonly JeopardyDbContext testContext;
        readonly IJeopardyRepository _service;
        readonly IMapper _mapper;
        readonly CategoriesModel model;

        public TestCategoriesController() {

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new JeopardyMappingProfile());
            });

            model = new CategoriesModel() { Id = 35, CategoryNameEn = "FiveCATS", CategoryNameFr = "TestCat 5 en francais" };

            var mapper = mappingConfig.CreateMapper(); // Use this mapper to instantiate your class
            _mapper = mapper;

            _service = new JeopardyRepositoryFake(testContext);
            _controller = new CategoriesController(_service, _mapper);

        }

        [Fact]
        public async void GetAllCategories_ShouldReturnAllCategories()
        {
            // Act
            var okResult = await _controller.Get();
            var okObjectResults = okResult.Result as OkObjectResult;            

            // Assert
            var items = Xunit.Assert.IsType<List<CategoriesModel>>(okObjectResults.Value);
            Assert.AreEqual(5, items.Count);
            Assert.IsNotNull(okResult);        
        }

        [Fact]
        public async void GetCategoryById_ShouldReturnCategoryById()
        {
            int id = 30;

            // Act
            var okResult = await _controller.GetById(id);
            var okResult2 = okResult.Result as OkObjectResult;
            
            // Assert
            var item = Xunit.Assert.IsType<CategoriesModel>(okResult2.Value);
            Assert.AreEqual(id, item.Id);
        }

        [Fact]
        public async void PostCategory_ShouldPostCategory()
        {
            var okResult = await _controller.Post(model);
            var okRouteResult = okResult.Result as CreatedAtRouteResult;

            Xunit.Assert.IsType<CreatedAtRouteResult>(okRouteResult);
            Xunit.Assert.NotNull(okRouteResult);            
        }






    }
}
