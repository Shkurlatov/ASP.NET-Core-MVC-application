using System.ComponentModel.DataAnnotations;

namespace School.Application.Models
{
    public class CuratorModel
    {
        [Required]
        public string Id { get; set; }

        public int? GroupId { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public GroupModel Group { get; set; }
    }
}
