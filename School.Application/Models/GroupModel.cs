using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using School.Domain.Entities;

namespace School.Application.Models
{
    public class GroupModel : BaseModel
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public CourseModel Course { get; set; }
        public ICollection<StudentModel> Students { get; private set; }
        public Curator Curator { get; set; }
    }
}
