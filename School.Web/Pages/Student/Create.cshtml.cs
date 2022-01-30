using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Application.Services;

namespace School.Web.Pages.Student
{
    public class CreateModel : PageModel
    {
        private readonly IService<StudentModel> _service;
        private readonly IService<GroupModel> _parentService;

        public CreateModel(IService<StudentModel> service, IService<GroupModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var groups = await _parentService.GetAll();
            ViewData["GroupId"] = new SelectList(groups, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public StudentModel Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Create(Student);
            return RedirectToPage("./Index");
        }
    }
}