using School.Core.Entities;
using Xunit;

namespace School.Core.Tests.Entities
{
    public class StudentTests
    {
        private int _testStudentId = 2;
        private int _testGroupId = 3;
        private string _testStudentFirstName = "First Name";
        private string _testStudentLastName = "Last Name";

        [Fact]
        public void Create_Student()
        {
            var student = Student.Create(_testStudentId, _testGroupId, _testStudentFirstName, _testStudentLastName);

            Assert.Equal(_testStudentId, student.Id);
            Assert.Equal(_testGroupId, student.GroupId);
            Assert.Equal(_testStudentFirstName, student.FirstName);
            Assert.Equal(_testStudentLastName, student.LastName);
        }
    }
}
