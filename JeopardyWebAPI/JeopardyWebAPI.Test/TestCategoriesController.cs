using JeopardyWebAPI.Controllers;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JeopardyWebAPI.Data.EFCore;
using Microsoft.AspNetCore.Mvc;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using AutoMapper;
using JeopardyWebAPI;
using NUnit.Framework;
using JeopardyWebAPI.Models;
using System;
using AutoMapper;
using JeopardyWebAPI.Data;
using System.Linq;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace JeopardyWebAPI.Test
{
    [TestClass]
    public class TestCategoriesController
    {
        CategoriesController _controller;
        JeopardyDbContext testContext;
        IJeopardyRepository _service;
        IMapper _mapper;
        CategoriesModel model;



        public TestCategoriesController() {

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new JeopardyMappingProfile());
            });

           /* var dbOption = new DbContextOptions<JeopardyDbContext>();
            var mockDbContext = new Mock<JeopardyDbContext>(dbOption);
            var mockConfig = new Mock<IConfiguration>();
            var instance = new JeopardyRepositoryFake(mockConfig.Object, mockDbContext.Object); */

            model = new CategoriesModel() { Id = 35, CategoryNameEn = "FiveCATS", CategoryNameFr = "TestCat 5 en francais" };

            var mapper = mappingConfig.CreateMapper(); // Use this mapper to instantiate your class
            _mapper = mapper;

            _service = new JeopardyRepositoryFake(testContext);
            _controller = new CategoriesController(_service, _mapper);

        }
        [Fact]
        public void SaveAsync_Categories()
        {
            var helper = new TestHelper();
            //Repositories with InMemory Database
            // var readRepo = helper.GetInMemoryJeopardyRepository();
            var repo = helper.GetInMemoryJeopardyRepository();

            //add mock data here
            repo.AddCategory(new Categories() { Id = 30, CategoryNameEn = "TestCat 1", CategoryNameFr = "TestCat 1 en francais" });
            repo.AddCategory(new Categories() { Id = 31, CategoryNameEn = "TestCat 2", CategoryNameFr = "TestCat 2 en francais" });
            repo.AddCategory(new Categories() { Id = 32, CategoryNameEn = "TestCat 3", CategoryNameFr = "TestCat 3 en francais" });
            repo.AddCategory(new Categories() { Id = 33, CategoryNameEn = "TestCat 4", CategoryNameFr = "TestCat 4 en francais" });
            repo.AddCategory(new Categories() { Id = 34, CategoryNameEn = "TestCat 5", CategoryNameFr = "TestCat 5 en francais" });
            //commit insert

            repo.SaveChangesAsync().GetAwaiter();

           //var result = repo.
        }

        [Fact]
        public async void GetAllCategories_ShouldReturnAllCategories()
        {

            // Act
            var okResult = await _controller.Get();
            var okResult2 = okResult.Result as OkObjectResult;
            // _controller.Get().Result as OkObjectResult;

            //var items = Assert.IsType<List<ShoppingItem>>(okResult.Value);
            // Assert.Equal(3, items.Count);

            // Assert
            var items = Xunit.Assert.IsType<List<CategoriesModel>>(okResult2.Value);
            Assert.AreEqual(5, items.Count);
           // Assert.IsNotNull(okResult);
        
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
          //  var okResult2 = okResult.Result as OkObjectResult;

            Xunit.Assert.IsType<CreatedAtRouteResult>(okResult);
        }






    }
}
