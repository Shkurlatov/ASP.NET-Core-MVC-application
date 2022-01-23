using School.Application.Exceptions;
using School.Application.Services;
using School.Core.Entities;
using School.Core.Interfaces;
using School.Core.Repositories;
using School.Core.Repositories.Base;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace School.Application.Tests.Services
{
    public class StudentTests
    {
        private Mock<IStudentRepository> _mockStudentRepository;
        private Mock<IRepository<Group>> _mockGroupRepository;
        private Mock<IAppLogger<StudentService>> _mockAppLogger;

        public StudentTests()
        {
            _mockStudentRepository = new Mock<IStudentRepository>();
            _mockGroupRepository = new Mock<IRepository<Group>>();
            _mockAppLogger = new Mock<IAppLogger<StudentService>>();
        }      

        [Fact]
        public async Task Get_Student_List()
        {
            var group = Group.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            var student1 = Student.Create(It.IsAny<int>(), group.Id, It.IsAny<string>(), It.IsAny<string>());
            var student2 = Student.Create(It.IsAny<int>(), group.Id, It.IsAny<string>(), It.IsAny<string>());

            _mockGroupRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(group);
            _mockStudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(student1);
            _mockStudentRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(student2);

            var studentService = new StudentService(_mockStudentRepository.Object, _mockAppLogger.Object);
            var studentList = await studentService.GetStudentList();

            _mockStudentRepository.Verify(x => x.GetStudentListAsync(), Times.Once);
        }
    }
}
