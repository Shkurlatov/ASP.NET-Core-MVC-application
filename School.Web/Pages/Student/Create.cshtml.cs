using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly IStudentPageService _studentPageService;

        public CreateModel(IStudentPageService studentPageService)
        {
            _studentPageService = studentPageService ?? throw new ArgumentNullException(nameof(studentPageService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var groups = await _studentPageService.GetGroups();
            ViewData["GroupId"] = new SelectList(groups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public StudentViewModel Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student = await _studentPageService.CreateStudent(Student);
            return RedirectToPage("./Index");
        }
    }
}