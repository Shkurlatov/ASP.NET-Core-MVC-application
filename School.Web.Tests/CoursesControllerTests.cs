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
    public class CoursesControllerTests
    {
        private readonly IStudyService<CourseModel> _courseServiceFake = A.Fake<IStudyService<CourseModel>>();
        private readonly CoursesController _sut;
        private readonly List<CourseModel> _courses;
        private readonly int _courseId = 1;
        private readonly int _notFoundStatusCode = 404;

        public CoursesControllerTests()
        {
            _sut = CreateSut(_courseServiceFake);
            _courses = GetTestModels();
        }

        [Fact]
        public void Index_ReturnViewResult()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetAll())
                .Returns(new List<CourseModel>());

            //Act
            IActionResult result = _sut.Index(default, default).Result;

            //Assert
            result
                .Should()
                .BeOfType<ViewResult>();
        }

        [Fact]
        public void Index_ReturnTypeIsListOfCourseModel()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetAll())
                .Returns(new List<CourseModel>());

            //Act
            IActionResult result = _sut.Index(default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .Should()
                .BeOfType<List<CourseModel>>();
        }

        [Fact]
        public void Index_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetAll())
                .Returns(new List<CourseModel>());

            //Act
            IActionResult result = _sut.Index(default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .ViewName
                .Should()
                .BeNull();
        }

        [Fact]
        public void Index_ReturnTestListOfCourseModel()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetAll())
                .Returns(_courses);

            //Act
            IActionResult result = _sut.Index(default, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<CourseModel>>()
                .Should()
                .HaveCount(2);
        }

        [Fact]
        public void Index_ReceiveId_ReturnCourseModelById()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetById(A<int>.Ignored))
                .Returns(_courses.First());

            //Act
            IActionResult result = _sut.Index(default, _courseId).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<CourseModel>>()
                .Should()
                .HaveCount(1);
        }

        [Fact]
        public void Index_ReceiveSearchTerm_ReturnListOfCourseModelBySearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            A.CallTo(() => _courseServiceFake.GetBySearch(A<string>.Ignored))
                .Returns(_courses.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            IActionResult result = _sut.Index(searchTerm, default).Result;

            //Assert
            result
                .As<ViewResult>()
                .Model
                .As<List<CourseModel>>()
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
        public void Create_ReceiveCourseModel_RedirectToIndexView()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.Create(A<CourseModel>.Ignored));

            //Act
            IActionResult result = _sut.Create(_courses.First()).Result;

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
        public void Edit_CourseExists_ReturnDefaultViewName()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.GetById(A<int>.Ignored))
                .Returns(_courses.First());

            //Act
            IActionResult result = _sut.Edit(_courseId).Result;

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
        public void DeleteConfirm_ReceiveCourseModel_RedirectToIndexView()
        {
            //Arrange
            A.CallTo(() => _courseServiceFake.Delete(A<CourseModel>.Ignored));

            //Act
            IActionResult result = _sut.DeleteConfirmed(_courses.First()).Result;

            //Assert
            result
                .Should()
                .BeOfType<RedirectToActionResult>();
        }

        private CoursesController CreateSut(IStudyService<CourseModel> courseServiceFake)
        {
            return new CoursesController(courseServiceFake);
        }

        private List<CourseModel> GetTestModels()
        {
            var models = new List<CourseModel>
            {
                new CourseModel()
                {
                    Id = 1,
                    Name = "Course Name 1",
                    Description = "Course Description 1"
                },
                new CourseModel()
                {
                    Id = 2,
                    Name = "Course Name 2",
                    Description = "Course Description 2"
                }
            };
            return models;
        }
    }
}
