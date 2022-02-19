using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using School.Application.Services.Studies;
using School.Application.Models.Studies;
using School.Domain.Interfaces.Studies;
using School.Domain.Entities.Studies;

namespace School.Application.Tests
{
    public class CourseServiceTests
    {
        private readonly Mock<IStudyRepository<Course>> _courseRepoMock = new Mock<IStudyRepository<Course>>();
        private readonly CourseService _sut;
        private readonly List<Course> _testEntities;
        private readonly CourseModel _testModel;
        private readonly int _testId;

        public CourseServiceTests()
        {
            _sut = new CourseService(_courseRepoMock.Object);
            _testEntities = GetTestEntities();
            _testModel = GetTestModel();
            _testId = 1;
        }

        [Fact]
        public async Task GetAll_ReturnTestListOfCourse()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(_testEntities);

            //Act
            var courses = await _sut.GetAll();

            //Assert
            _courseRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.Equal(courses.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetById_ReceiveCourseId_ReturnCourseWhenCourseExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(_testId))
                .ReturnsAsync(_testEntities.First());

            //Act
            var course = await _sut.GetById(_testId);

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Equal(_testId, course.Id);
        }

        [Fact]
        public async Task GetById_ReceiveCourseId_ReturnNullWhenCourseDoesNotExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var course = await _sut.GetById(_testId);

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Null(course);
        }

        [Fact]
        public async Task GetBySearch_ReceiveSearchTerm_ReturnListOfCourseContainsSearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            _courseRepoMock.Setup(x => x.GetBySearchAsync(searchTerm))
                .ReturnsAsync(_testEntities.Where(c => c.Name.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            var courses = await _sut.GetBySearch(searchTerm);

            //Assert
            _courseRepoMock.Verify(x => x.GetBySearchAsync(searchTerm), Times.Once);
            Assert.Equal(courses.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetByParent_ReceiveParentId_ReturnNotImplementedException()
        {
            //Arrange

            //Act
            await Assert.ThrowsAsync<NotImplementedException>(async () => await _sut.GetByParent(It.IsAny<int>()));

            //Assert
        }

        [Fact]
        public async Task Create_ReceiveCourseModel_SuccessefulWhenCourseDoesNotExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await _sut.Create(_testModel);

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task Create_ReceiveCourseModel_ReturnApplicationExceptionWhenCourseExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(c => c.Id == _testModel.Id).FirstOrDefault());

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Create(_testModel));

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.AddAsync(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public async Task Update_ReceiveCourseModel_SuccessefulWhenCourseExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(c => c.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Update(_testModel);

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReceiveCourseModel_ReturnApplicationExceptionWhenCourseDoesNotExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Update(_testModel));

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Course>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ReceiveCourseModel_SuccessefulWhenCourseExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(c => c.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Delete(_testModel);

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReceiveCourseModel_ReturnApplicationExceptionWhenCourseDoesNotExists()
        {
            //Arrange
            _courseRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Delete(_testModel));

            //Assert
            _courseRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _courseRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Course>()), Times.Never);
        }

        private List<Course> GetTestEntities()
        {
            var entities = new List<Course>
            {
                new Course()
                {
                    Id = 1,
                    Name = "Course Name 1",
                    Description = "Course Description 1"
                },
                new Course()
                {
                    Id = 2,
                    Name = "Course Name 2",
                    Description = "Course Description 2"
                }
            };
            return entities;
        }

        private CourseModel GetTestModel()
        {
            var model = new CourseModel
            {
                Id = 1,
                Name = "Course Name 1",
                Description = "Course Description 1"
            };
            return model;
        }
    }
}
