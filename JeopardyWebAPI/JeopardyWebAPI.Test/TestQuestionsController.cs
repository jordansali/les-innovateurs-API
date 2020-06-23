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
using System.Linq;

namespace JeopardyWebAPI.Test
{
    [TestClass]
    public class TestQuestionsController
    {

        readonly QuestionsController _controller;
        private readonly JeopardyDbContext testContext;
        readonly IJeopardyRepository _service;
        readonly IMapper _mapper;
        readonly QuestionsModel model;
        readonly QuestionsModel updateModel;

        public TestQuestionsController()
        {

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new JeopardyMappingProfile());
            });

            model = new QuestionsModel() { Id = 45, QuestionEn = "TestQuestion 1", QuestionFr = "TestQuestion 1 en francais", AnswerEn = "TestAnswer 1", AnswerFr = "TestAnswer 1 en francais", CategoryId = 30, Hint = "Test Hint 1", Points = 100, TimeLimit = 30 };

            updateModel = new QuestionsModel() { Id = 32, QuestionEn = "TestQuestion 112434", QuestionFr = "TestQuestion 112434 en francais", AnswerEn = "TestAnswer 1", AnswerFr = "TestAnswer 1 en francais", CategoryId = 30, Hint = "Test Hint 1", Points = 100, TimeLimit = 30 };

            var mapper = mappingConfig.CreateMapper(); // Use this mapper to instantiate your class
            _mapper = mapper;

            _service = new JeopardyRepositoryFake(testContext);

            _controller = new QuestionsController(_service, _mapper);

        }

        [Fact]
        public async void GetAllQuestions_ShouldReturnAllQuestions()
        {
            // Act
            var okResult = await _controller.Get();
            var okObjectResults = okResult.Result as OkObjectResult;

            // Assert
            var items = Xunit.Assert.IsType<List<QuestionsModel>>(okObjectResults.Value);
            Assert.AreEqual(5, items.Count);
            Assert.IsNotNull(okResult);
        }

        [Fact]
        public async void GetQuestionsByPoints_ShouldReturnQuestionsByPoints()
        {
            int points = 300; 

            // Act
            var okResult = await _controller.GetByPoints(points);
            var okObjectResult = okResult.Result as OkObjectResult;

            // Assert
            var item = Xunit.Assert.IsType<List<QuestionsModel>>(okObjectResult.Value);
            Assert.AreEqual(points, item.FirstOrDefault().Points);
        }

        [Fact]
        public async void PostQuestions_ShouldAddNewQuestion()
        {
            var okResult = await _controller.Post(model);
            var okRouteResult = okResult.Result as CreatedAtRouteResult;

            Xunit.Assert.IsType<CreatedAtRouteResult>(okRouteResult);
            Xunit.Assert.NotNull(okRouteResult);
        }

        [Fact]
        public async void PostBadQuestions_ShouldReturnBadRequest()
        {
            var badModel = new QuestionsModel() { Id = 45, QuestionEn = "TestQuestion 1", QuestionFr = "TestQuestion 1 en francais", AnswerEn = "TestAnswer 1", AnswerFr = "TestAnswer 1 en francais", CategoryId = 12, Hint = "Test Hint 1", Points = 100, TimeLimit = 30 };

            var okResult = await _controller.Post(badModel);
            var okRouteResult = okResult.Result as BadRequestObjectResult;

            Xunit.Assert.IsType<BadRequestObjectResult>(okRouteResult);
            Xunit.Assert.NotNull(okRouteResult);
        }

        [Fact]
        public async void PutCategory_UpdatesCategory()
        {
            int Id = 32;

            var okResult = await _controller.Put(Id, updateModel);
            var okRouteResult = okResult.Result as OkObjectResult;

            Xunit.Assert.IsType<OkObjectResult>(okRouteResult);
            Xunit.Assert.NotNull(okRouteResult);
        }

        [Fact]
        public async void DeleteCategory_RemovesCategory()
        {
            int Id = 31;

            var okResult = await _controller.Delete(Id);
            var okRouteResult = okResult.Result as NoContentResult;

            Xunit.Assert.IsType<NoContentResult>(okRouteResult);
        }

    }
}
