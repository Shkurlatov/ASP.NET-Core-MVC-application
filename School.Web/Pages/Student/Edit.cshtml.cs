using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Web.ViewModels;
using School.Web.Interfaces;

namespace School.Web.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly IStudentPageService _studentPageService;

        public EditModel(IStudentPageService studentPageService)
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
            
            ViewData["GroupId"] = new SelectList(await _studentPageService.GetGroups(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try
            {
                await _studentPageService.UpdateStudent(Student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            var student = _studentPageService.GetStudentById(id);
            return student != null;            
        }
    }
}
