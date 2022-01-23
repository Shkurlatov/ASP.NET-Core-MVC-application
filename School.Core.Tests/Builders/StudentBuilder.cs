using School.Core.Entities;
using System.Collections.Generic;

namespace School.Core.Tests.Builders
{
    public class StudentBuilder
    {
        public int StudentId1 => 123;
        public int StudentId2 => 124;
        public int StudentId3 => 125;
        public int StudentGroupId1 => 11;
        public int StudentGroupId2 => 18;
        public int StudentGroupId3 => 27;
        public string StudentFirstName1 => "First Name 1";
        public string StudentFirstName2 => "First Name 2";
        public string StudentFirstName3 => "First Name 3";
        public string StudentLastName1 => "Last Name 1";
        public string StudentLastName2 => "Last Name 2";
        public string StudentLastName3 => "Last Name 3";

        public List<Student> GetStudentCollection()
        {
            return new List<Student>()
            {
                Student.Create(StudentId1, StudentGroupId1, StudentFirstName1, StudentLastName1),
                Student.Create(StudentId2, StudentGroupId2, StudentFirstName2, StudentLastName2),
                Student.Create(StudentId3, StudentGroupId3, StudentFirstName3, StudentLastName3)
            };
        }
    }
}
