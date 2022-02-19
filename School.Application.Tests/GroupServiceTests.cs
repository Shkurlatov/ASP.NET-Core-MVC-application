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
    public class GroupServiceTests
    {
        private readonly Mock<IStudyRepository<Group>> _groupRepoMock = new Mock<IStudyRepository<Group>>();
        private readonly GroupService _sut;
        private readonly List<Group> _testEntities;
        private readonly GroupModel _testModel;
        private readonly int _testId;

        public GroupServiceTests()
        {
            _sut = new GroupService(_groupRepoMock.Object);
            _testEntities = GetTestEntities();
            _testModel = GetTestModel();
            _testId = 1;
        }

        [Fact]
        public async Task GetAll_ReturnTestListOfGroup()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(_testEntities);

            //Act
            var groups = await _sut.GetAll();

            //Assert
            _groupRepoMock.Verify(x => x.GetAllAsync(), Times.Once);
            Assert.Equal(groups.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetById_ReceiveGroupId_ReturnCourseWhenGroupExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(_testId))
                .ReturnsAsync(_testEntities.First());

            //Act
            var group = await _sut.GetById(_testId);

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Equal(_testId, group.Id);
        }

        [Fact]
        public async Task GetById_ReceiveGroupId_ReturnNullWhenGroupDoesNotExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            var group = await _sut.GetById(_testId);

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testId), Times.Once);
            Assert.Null(group);
        }

        [Fact]
        public async Task GetBySearch_ReceiveSearchTerm_ReturnListOfGroupContainsSearchTerm()
        {
            //Arrange
            string searchTerm = "n";
            _groupRepoMock.Setup(x => x.GetBySearchAsync(searchTerm))
                .ReturnsAsync(_testEntities.Where(g => g.Name.ToLower().Contains(searchTerm.ToLower())).ToList());

            //Act
            var groups = await _sut.GetBySearch(searchTerm);

            //Assert
            _groupRepoMock.Verify(x => x.GetBySearchAsync(searchTerm), Times.Once);
            Assert.Equal(groups.Count(), _testEntities.Count());
        }

        [Fact]
        public async Task GetByParent_ReceiveParentId_ReturnListOfGroupWithEqualParentId()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByParentAsync(_testModel.CourseId))
                .ReturnsAsync(() => _testEntities.Where(g => g.CourseId == _testModel.CourseId).ToList());

            //Act
            var groups = await _sut.GetByParent(_testModel.CourseId);

            //Assert
            _groupRepoMock.Verify(x => x.GetByParentAsync(_testModel.CourseId), Times.Once);
            Assert.Single(groups);
        }

        [Fact]
        public async Task Create_ReceiveGroupModel_SuccessefulWhenGroupDoesNotExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await _sut.Create(_testModel);

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.AddAsync(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public async Task Create_ReceiveGroupModel_ReturnApplicationExceptionWhenGroupExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(g => g.Id == _testModel.Id).FirstOrDefault());

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Create(_testModel));

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.AddAsync(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public async Task Update_ReceiveGroupModel_SuccessefulWhenGroupExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(g => g.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Update(_testModel);

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public async Task Update_ReceiveGroupModel_ReturnApplicationExceptionWhenGroupDoesNotExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Update(_testModel));

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.UpdateAsync(It.IsAny<Group>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ReceiveGroupModel_SuccessefulWhenGroupExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(_testModel.Id))
                .ReturnsAsync(() => _testEntities.Where(g => g.Id == _testModel.Id).FirstOrDefault());

            //Act
            await _sut.Delete(_testModel);

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Group>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReceiveGroupModel_ReturnApplicationExceptionWhenGroupDoesNotExists()
        {
            //Arrange
            _groupRepoMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            //Act
            await Assert.ThrowsAsync<ApplicationException>(async () => await _sut.Delete(_testModel));

            //Assert
            _groupRepoMock.Verify(x => x.GetByIdAsync(_testModel.Id), Times.Once);
            _groupRepoMock.Verify(x => x.DeleteAsync(It.IsAny<Group>()), Times.Never);
        }

        private List<Group> GetTestEntities()
        {
            var entities = new List<Group>
            {
                new Group()
                {
                    Id = 1,
                    CourseId = 1,
                    Name = "Group Name 1"
                },
                new Group()
                {
                    Id = 2,
                    CourseId = 2,
                    Name = "Group Name 2"
                }
            };
            return entities;
        }

        private GroupModel GetTestModel()
        {
            var model = new GroupModel
            {
                Id = 1,
                CourseId = 1,
                Name = "Group Name 1"
            };
            return model;
        }
    }
}
