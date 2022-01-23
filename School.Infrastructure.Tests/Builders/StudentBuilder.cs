using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace School.Infrastructure.Tests.Builders
{
    public class StudentBuilder
    {
        private Student _student;
        public int TestStudentId => 123;
        public int TestGroupId => 18;
        public string TestStudentFirstName => "Test First Name";
        public string TestStudentLastName => "Test Last Name";

        public StudentBuilder()
        {
            _student = WithDefaultValues();

        }

        public Student Build()
        {
            return _student;
        }

        public Student WithDefaultValues()
        {
            return Student.Create(TestStudentId, TestGroupId, TestStudentFirstName, TestStudentLastName);
        }
    }
}
