using School.Domain.Entities.Base;

namespace School.Domain.Entities
{
    public class Student : Entity
    {
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Group Group { get; set; }

        public static Student Create(int studentId, int groupId, string firstName, string lastName)
        {
            var student = new Student
            {
                Id = studentId,
                GroupId = groupId,
                FirstName = firstName,
                LastName = lastName
            };
            return student;
        }
    }
}
