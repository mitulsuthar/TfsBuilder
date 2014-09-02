using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TfsBuilder;
using TfsBuilder.ViewModels;
using FakeItEasy;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TfsBuilder.Lib;
namespace TfsBuilder.Tests
{
    [TestClass]
    public class BuildViewModelTest
    {
        [TestMethod]
        public void BuildViewModel_ShouldBeAbleToGetTfsServerList()
        {
            //Arrange             
            var fakeRepo = A.Fake<ITfsRepository>();            
            A.CallTo(() => fakeRepo.GetTfsServerList()).Returns<List<string>>(new List<string>() { 
               "http://visualstudioonline.com",
                "http://microsoft.com" 
            });
            
            var fakeBuildViewModel = new BuildViewModel(fakeRepo);

            Assert.IsTrue(fakeBuildViewModel.TfsServerList.Count > 0);            
        }


    }
}
