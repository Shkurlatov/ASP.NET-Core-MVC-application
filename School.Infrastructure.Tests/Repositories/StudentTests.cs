using School.Infrastructure.Data;
using School.Infrastructure.Repository;
using School.Infrastructure.Tests.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace School.Infrastructure.Tests.Repositories
{
    public class StudentTests
    {
        private readonly SchoolContext _schoolContext;
        private readonly StudentRepository _studentRepository;
        private readonly ITestOutputHelper _output;
        private StudentBuilder StudentBuilder { get; } = new StudentBuilder();
        private GroupBuilder GroupBuilder { get; } = new GroupBuilder();

        public StudentTests(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<SchoolContext>()
                .UseInMemoryDatabase(databaseName: "School")
                .Options;
            _schoolContext = new SchoolContext(dbOptions);
            _studentRepository = new StudentRepository(_schoolContext);
        }

        [Fact]
        public async Task Get_Student_By_Group()
        {
            var existingStudent = StudentBuilder.WithDefaultValues();
            _schoolContext.Students.Add(existingStudent);
            var existingGroup = GroupBuilder.WithDefaultValues();
            _schoolContext.Groups.Add(existingGroup);
            _schoolContext.SaveChanges();

            var groupId = existingStudent.GroupId;
            _output.WriteLine($"GroupId: {groupId}");
            
            var studentListFromRepo = await _studentRepository.GetStudentByGroupAsync(groupId);
            Assert.Equal(StudentBuilder.TestGroupId, studentListFromRepo.ToList().First().GroupId);
        }
    }
}
