using System.Collections.Generic;
using School.Application.Models.Users;

namespace School.Application.Models.Studies
{
    public class GroupModel : BaseModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseModel Course { get; set; }
        public ICollection<StudentModel> Students { get; private set; }
        public CuratorModel Curator { get; set; }
    }
}
