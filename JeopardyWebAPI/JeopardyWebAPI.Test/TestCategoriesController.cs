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

namespace JeopardyWebAPI.Test
{
    [TestClass]
    public class TestCategoriesController
    {
        CategoriesController _controller;
        IJeopardyRepository _service;
        IMapper _mapper;


        public TestCategoriesController() {

            _service = new JeopardyRepositoryFake();
            _controller = new CategoriesController(_service, _mapper);

        }


        [Fact]
        public void GetAllCategories_ShouldReturnAllCategories()
        {

            // Act
            var okResult = _controller.Get();
      
            // Assert
            Assert.IsNotNull(okResult);
        
        }


    }
}
