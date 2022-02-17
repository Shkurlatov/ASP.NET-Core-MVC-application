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
    public class StudentsControllerTests
    {
        private readonly IStudyService<StudentModel> _studentServiceFake = A.Fake<IStudyService<StudentModel>>();
        private readonly StudentsController _sut;
        private readonly List<StudentModel> _students;
        private readonly int _studentId = 1;
        private readonly int _notFoundStatusCode = 404;

        public StudentsControllerTests()
        {
            _sut = CreateSut(_studentServiceFake);
            _students = GetTestModels();
        }

        [Fact]
        public void Index_ReturnViewResult()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetAll())
                .Returns(new List<StudentModel>());

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void Index_ReturnTypeIsListOfStudentModel()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetAll())
                .Returns(new List<StudentModel>());

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .Should()
                .BeOfType<List<StudentModel>>();
        }

        [Fact]
        public void Index_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetAll())
                .Returns(new List<StudentModel>());

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
        public void Index_ReturnTestListOfStudentModel()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetAll())
                .Returns(_students);

            //Act
            IActionResult result = _sut.Index(default, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<StudentModel>>()
                .Should()
                .HaveCount(2);
        }

        [Fact]
        public void Index_ReceiveId_ReturnStudentModelById()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetById(A<int>.Ignored))
                .Returns(_students.First());

            //Act
            IActionResult result = _sut.Index(default, _studentId, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<StudentModel>>()
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public void Index_ReceiveParentId_ReturnListOfStudentModelByParentId()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetByParent(A<int>.Ignored))
                .Returns(_students.Where(c => c.GroupId == _students.First().GroupId).ToList());

            //Act
            IActionResult result = _sut.Index(default, default, _students.First().GroupId).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<StudentModel>>()
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public void Index_ReceiveSearchTerm_ReturnListOfStudentModelBySearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            A.CallTo(() => _studentServiceFake.GetBySearch(A<string>.Ignored))
                .Returns(_students.Where(c => c.LastName.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            IActionResult result = _sut.Index(searchTerm, default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<StudentModel>>()
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
        public void Edit_StudentExists_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.GetById(A<int>.Ignored))
                .Returns(_students.First());

            //Act
            IActionResult result = _sut.Edit(_studentId).Result;

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
        public void DeleteConfirm_ValidStudentId_RedirectToIndexView()
        {
            //Arrange
            A.CallTo(() => _studentServiceFake.Delete(A<StudentModel>.Ignored));

            //Act
            IActionResult result = _sut.DeleteConfirmed(_students.First()).Result;

            //Assert
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
        }

        private StudentsController CreateSut(IStudyService<StudentModel> studentServiceFake)
        {
            return new StudentsController(studentServiceFake, A.Fake<IStudyService<GroupModel>>());
        }

        private List<StudentModel> GetTestModels()
        {
            var models = new List<StudentModel>
            {
                new StudentModel()
                {
                    Id = 1,
                    GroupId = 1,
                    FirstName = "First Name 1",
                    LastName = "Last Name 1"
                },
                new StudentModel()
                {
                    Id = 2,
                    GroupId = 2,
                    FirstName = "First Name 2",
                    LastName = "Last Name 2"
                }
            };
            return models;
        }
    }
}
