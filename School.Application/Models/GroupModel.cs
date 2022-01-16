using School.Application.Models.Base;

namespace School.Application.Models
{
    public class GroupModel : BaseModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseModel Course { get; set; }
    }
}
