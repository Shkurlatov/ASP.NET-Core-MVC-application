using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace School.Application.Models
{
    public class CourseModel : BaseModel
    {
        [Required]
        [MinLength(2)]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        public ICollection<GroupModel> Groups { get; private set; }
    }
}
