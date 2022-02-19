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
    public class StudentServiceTests
    {
        private readonly Mock<IStudyRepository<Student>> _studentRepoMock = new Mock<IStudyRepository<Student>>();
        private readonly StudentService _sut;
        private readonly List<Student> _testEntities;
        private readonly StudentModel _testModel;
        private readonly int _testId;

        public StudentServiceTests()
        {
            _sut = new StudentService(_studentRepoMock.Object);
            _testEntities = GetTestEntities();
            _testModel = GetTestModel();
            _testId = 1;
        }

        [Fact]
        public async Task GetAll_ReturnTestListOfStudent()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(_testEntities);

            //Act
            var students = await _sut.GetAll();

            //Assert
            _studentRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.Equal(students.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetById_ReceiveStudentId_ReturnCourseWhenStudentExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(_testId))
                .ReturnsAsync(_testEntities.First());

            //Act
            var student = await _sut.GetById(_testId);

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Equal(_testId, student.Id);
        }

        [Fact]
        public async Task GetById_ReceiveStudentId_ReturnNullWhenStudentDoesNotExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var student = await _sut.GetById(_testId);

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Null(student);
        }

        [Fact]
        public async Task GetBySearch_ReceiveSearchTerm_ReturnListOfStudentContainsSearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            _studentRepoMock.Setup(x => x.GetBySearchAsync(searchTerm))
                .ReturnsAsync(_testEntities.Where(s => s.FirstName.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            var students = await _sut.GetBySearch(searchTerm);

            //Assert
            _studentRepoMock.Verify(x => x.GetBySearchAsync(searchTerm), Times.Once);
            Assert.Equal(students.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetByParent_ReceiveParentId_ReturnListOfStudentWithEqualParentId()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByParentAsync(_testModel.GroupId))
                .ReturnsAsync(() => _testEntities.Where(s => s.GroupId == _testModel.GroupId).ToList());

            //Act
            var students = await _sut.GetByParent(_testModel.GroupId);

            //Assert
            _studentRepoMock.Verify(x => x.GetByParentAsync(_testModel.GroupId), Times.Once);
            Assert.Single(students);
        }

        [Fact]
        public async Task Create_ReceiveStudentModel_SuccessefulWhenStudentDoesNotExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await _sut.Create(_testModel);

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task Create_ReceiveStudentModel_ReturnApplicationExceptionWhenStudentExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(s => s.Id == _testModel.Id).FirstOrDefault());

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Create(_testModel));

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public async Task Update_ReceiveStudentModel_SuccessefulWhenStudentExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(s => s.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Update(_testModel);

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReceiveStudentModel_ReturnApplicationExceptionWhenStudentDoesNotExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Update(_testModel));

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Student>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ReceiveStudentModel_SuccessefulWhenStudentExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(s => s.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Delete(_testModel);

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Student>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReceiveStudentModel_ReturnApplicationExceptionWhenStudentDoesNotExists()
        {
            //Arrange
            _studentRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Delete(_testModel));

            //Assert
            _studentRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _studentRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Student>()), Times.Never);
        }

        private List<Student> GetTestEntities()
        {
            var entities = new List<Student>
            {
                new Student()
                {
                    Id = 1,
                    GroupId = 1,
                    FirstName = "Firs Name 1",
                    LastName = "Last Name 1"
                },
                new Student()
                {
                    Id = 2,
                    GroupId = 2,
                    FirstName = "Firs Name 2",
                    LastName = "Last Name 2"
                }
            };
            return entities;
        }

        private StudentModel GetTestModel()
        {
            var model = new StudentModel
            {
                Id = 1,
                GroupId = 1,
                FirstName = "Firs Name 1",
                LastName = "Last Name 1"
            };
            return model;
        }
    }
}
