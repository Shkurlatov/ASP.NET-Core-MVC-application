using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Student
{
    public class DetailsModel : PageModel
    {
        private readonly IStudentPageService _studentPageService;

        public DetailsModel(IStudentPageService studentPageService)
        {
            _studentPageService = studentPageService ?? throw new ArgumentNullException(nameof(studentPageService));
        }       

        public StudentViewModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            //Student = await _studentPageService.GetStudentById(studentId.Value);
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
