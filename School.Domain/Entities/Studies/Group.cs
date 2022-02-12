using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using School.Domain.Entities.Users;

namespace School.Domain.Entities.Studies
{
    public class Group : Entity
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")] 
        public string Name { get; set; }

        public Course Course { get; set; }

        public ICollection<Student> Students { get; private set; }

        public Curator Curator { get; set; }
    }
}
