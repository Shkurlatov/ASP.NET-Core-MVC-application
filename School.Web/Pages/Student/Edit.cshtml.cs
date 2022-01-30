using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Web.Pages.Student
{
    public class EditModel : PageModel
    {
        private readonly IService<StudentModel> _service;
        private readonly IService<GroupModel> _parentService;

        public EditModel(IService<StudentModel> service, IService<GroupModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        [BindProperty]
        public StudentModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            Student = await _service.GetById(studentId.Value);
            if (Student == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(await _parentService.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Update(Student);
            return RedirectToPage("./Index");
        }
    }
}
