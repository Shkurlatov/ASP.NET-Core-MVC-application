using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace School.Web.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        [Display(Name = "Course Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<CourseViewModel> Students { get; private set; }
    }
}
