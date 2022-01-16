using School.Application.Models.Base;

namespace School.Application.Models
{
    public class StudentModel : BaseModel
    {
        public int GroupId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GroupModel Group { get; set; }
    }
}
