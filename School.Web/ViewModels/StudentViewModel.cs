using System.ComponentModel.DataAnnotations;

namespace School.Web.ViewModels
{
    public class StudentViewModel : BaseViewModel
    {
        public int GroupId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public GroupViewModel Group { get; set; }
    }
}
