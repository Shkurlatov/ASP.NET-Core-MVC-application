using System.Collections.Generic;

namespace School.Application.Models
{
    public class CourseModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<GroupModel> Groups { get; private set; }
    }
}
