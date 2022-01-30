using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Application.Services;

namespace School.Web.Pages.Group
{
    public class CreateModel : PageModel
    {
        private readonly IService<GroupModel> _service;
        private readonly IService<CourseModel> _parentService;

        public CreateModel(IService<GroupModel> service, IService<CourseModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var courses = await _parentService.GetAll();
            ViewData["CourseId"] = new SelectList(courses, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public GroupModel Group { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Create(Group);
            return RedirectToPage("./Index");
        }
    }
}