using School.Core.Specifications;
using School.Core.Tests.Builders;
using System.Linq;
using Xunit;

namespace School.Core.Tests.Specifications
{
    public class StudentSpecificationTests
    {
        private StudentBuilder StudentBuilder { get; } = new StudentBuilder();

        [Fact]
        public void Matches_Student_With_Id_Spec()
        {
            var spec = new StudentByIdSpecification(StudentBuilder.StudentId1);

            var result = StudentBuilder.GetStudentCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(StudentBuilder.StudentGroupId1, result.GroupId);
            Assert.Equal(StudentBuilder.StudentFirstName1, result.FirstName);
            Assert.Equal(StudentBuilder.StudentLastName1, result.LastName);
        }

        [Fact]
        public void Matches_Student_With_Group_Spec()
        {
            var spec = new StudentByGroupSpecification(StudentBuilder.StudentGroupId1);

            var result = StudentBuilder.GetStudentCollection()
                .AsQueryable()
                .FirstOrDefault(spec.Criteria);

            Assert.NotNull(result);
            Assert.Equal(StudentBuilder.StudentId1, result.Id);
            Assert.Equal(StudentBuilder.StudentFirstName1, result.FirstName);
            Assert.Equal(StudentBuilder.StudentLastName1, result.LastName);
        }
    }
}
