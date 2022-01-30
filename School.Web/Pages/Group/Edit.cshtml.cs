using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Web.Pages.Group
{
    public class EditModel : PageModel
    {
        private readonly IService<GroupModel> _service;
        private readonly IService<CourseModel> _parentService;

        public EditModel(IService<GroupModel> service, IService<CourseModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        [BindProperty]
        public GroupModel Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            Group = await _service.GetById(groupId.Value);
            if (Group == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(await _parentService.GetAll(), "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _service.Update(Group);
            return RedirectToPage("./Index");
        }
    }
}
