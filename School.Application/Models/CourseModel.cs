using School.Service.Models.Base;
using System.Collections.Generic;

namespace School.Service.Models
{
    public class CourseModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CourseModel> Students { get; private set; }
    }
}
