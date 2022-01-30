
namespace School.Domain.Entities
{
    public class Student : Entity
    {
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Group Group { get; set; }
    }
}
