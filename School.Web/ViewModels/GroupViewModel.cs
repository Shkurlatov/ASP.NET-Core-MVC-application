using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace School.Web.ViewModels
{
    public class GroupViewModel : BaseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Group Name")]
        public string Name { get; set; }

        public CourseViewModel Course { get; set; }

        public ICollection<StudentViewModel> Students { get; private set; }
    }
}
