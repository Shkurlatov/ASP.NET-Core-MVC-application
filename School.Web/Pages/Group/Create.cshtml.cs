using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Group
{
    public class CreateModel : PageModel
    {
        private readonly IGroupPageService _groupPageService;

        public CreateModel(IGroupPageService groupPageService)
        {
            _groupPageService = groupPageService ?? throw new ArgumentNullException(nameof(groupPageService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var courses = await _groupPageService.GetCourses();
            ViewData["CourseId"] = new SelectList(courses, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public GroupViewModel Group { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Group = await _groupPageService.CreateGroup(Group);
            return RedirectToPage("./Index");
        }
    }
}