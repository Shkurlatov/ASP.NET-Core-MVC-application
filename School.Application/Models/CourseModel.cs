using School.Application.Models.Base;

namespace School.Application.Models
{
    public class CourseModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
