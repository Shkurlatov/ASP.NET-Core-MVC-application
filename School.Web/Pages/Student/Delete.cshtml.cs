using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Student
{
    public class DeleteModel : PageModel
    {
        private readonly IStudentPageService _studentPageService;

        public DeleteModel(IStudentPageService studentPageService)
        {
            _studentPageService = studentPageService ?? throw new ArgumentNullException(nameof(studentPageService));
        }

        [BindProperty]
        public StudentViewModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            Student = await _studentPageService.GetStudentById(studentId.Value);
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            await _studentPageService.DeleteStudent(Student);          
            return RedirectToPage("./Index");
        }
    }
}
