using School.Service.Models.Base;
using System.Collections.Generic;

namespace School.Service.Models
{
    public class GroupModel : BaseModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseModel Course { get; set; }
        public ICollection<StudentModel> Students { get; private set; }
    }
}
