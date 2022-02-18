using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FakeItEasy;
using FluentAssertions;
using School.Application.Models.Studies;
using School.Domain.Interfaces.Studies;
using School.Controllers;

namespace School.Web.Tests
{
    public class GroupsControllerTests
    {
        private readonly IStudyService<GroupModel> _groupServiceFake = A.Fake<IStudyService<GroupModel>>();
        private readonly GroupsController _sut;
        private readonly List<GroupModel> _groups;
        private readonly int _groupId = 1;
        private readonly int _notFoundStatusCode = 404;

        public GroupsControllerTests()
        {
            _sut = CreateSut(_groupServiceFake);
            _groups = GetTestModels();
        }

        [Fact]
        public void Index_ReturnViewResult()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetAll())
                .Returns(new List<GroupModel>());

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void Index_ReturnTypeIsListOfGroupModel()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetAll())
                .Returns(new List<GroupModel>());

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .Should()
                .BeOfType<List<GroupModel>>();
        }

        [Fact]
        public void Index_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetAll())
                .Returns(new List<GroupModel>());

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .ViewName
                .Should()
                .BeNull();
        }

        [Fact]
        public void Index_ReturnTestListOfGroupModel()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetAll())
                .Returns(_groups);

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<GroupModel>>()
                .Should()
                .HaveCount(2);
        }

        [Fact]
        public void Index_ReceiveId_ReturnGroupModelById()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetById(A<int>.Ignored))
                .Returns(_groups.First());

            //Act
            IActionResult result = _sut.Index(default, _groupId, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<GroupModel>>()
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public void Index_ReceiveParentId_ReturnListOfGroupModelByParentId()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetByParent(A<int>.Ignored))
                .Returns(_groups.Where(c => c.CourseId == _groups.First().CourseId).ToList());

            //Act
            IActionResult result = _sut.Index(default, default, _groups.First().CourseId).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<GroupModel>>()
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public void Index_ReceiveSearchTerm_ReturnListOfGroupModelBySearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            A.CallTo(() => _groupServiceFake.GetBySearch(A<string>.Ignored))
                .Returns(_groups.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            IActionResult result = _sut.Index(searchTerm, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<GroupModel>>()
                .Should()
                .HaveCount(2);
        }

        [Fact]
        public void Details_ReceiveNullId_ReturnNotFoundStatusCode()
        {
            //Arrange

            //Act
            IActionResult result = _sut.Details(null).Result;

            //Assert
            result
                .As<NotFoundResult>()
                .StatusCode
                .Should()
                .Be(_notFoundStatusCode);
        }

        [Fact]
        public void Create_ReceiveGroupModel_RedirectToIndexView()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.Create(A<GroupModel>.Ignored));

            //Act
            IActionResult result = _sut.Create(_groups.First()).Result;

            //Assert
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
        }

        [Fact]
        public void Edit_ReceiveNullId_ReturnNotFoundStatusCode()
        {
            //Arrange

            //Act
            IActionResult result = _sut.Details(null).Result;

            //Assert
            result
                .As<NotFoundResult>()
                .StatusCode
                .Should()
                .Be(_notFoundStatusCode);
        }

        [Fact]
        public void Edit_GroupExists_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.GetById(A<int>.Ignored))
                .Returns(_groups.First());

            //Act
            IActionResult result = _sut.Edit(_groupId).Result;

            //Assert
            result
                .As<ViewResult>()
                .ViewName
                .Should()
                .Be("Edit");
        }

        [Fact]
        public void Delete_ReceiveNullId_ReturnNotFoundStatusCode()
        {
            //Arrange

            //Act
            IActionResult result = _sut.Delete(null).Result;

            //Assert
            result
                .As<NotFoundResult>()
                .StatusCode
                .Should()
                .Be(_notFoundStatusCode);
        }

        [Fact]
        public void DeleteConfirm_ReceiveGroupModel_RedirectToIndexView()
        {
            //Arrange
            A.CallTo(() => _groupServiceFake.Delete(A<GroupModel>.Ignored));

            //Act
            IActionResult result = _sut.DeleteConfirmed(_groups.First()).Result;

            //Assert
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
        }

        private GroupsController CreateSut(IStudyService<GroupModel> groupServiceFake)
        {
            return new GroupsController(groupServiceFake, A.Fake<IStudyService<CourseModel>>());
        }

        private List<GroupModel> GetTestModels()
        {
            var models = new List<GroupModel>
            {
                new GroupModel()
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Group Name 1"
                },
                new GroupModel()
                {
                    Id = 2,
                    CourseId = 2,
                    Name = "Group Name 2"
                }
            };
            return models;
        }
    }
}
