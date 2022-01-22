using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Web.ViewModels;
using School.Web.Interfaces;

namespace School.Web.Pages.Group
{
    public class EditModel : PageModel
    {
        private readonly IGroupPageService _groupPageService;

        public EditModel(IGroupPageService groupPageService)
        {
            _groupPageService = groupPageService ?? throw new ArgumentNullException(nameof(groupPageService));
        }

        [BindProperty]
        public GroupViewModel Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            Group = await _groupPageService.GetGroupById(groupId.Value);
            if (Group == null)
            {
                return NotFound();
            }
            
            ViewData["CourseId"] = new SelectList(await _groupPageService.GetCourses(), "Id", "Name");
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
                await _groupPageService.UpdateGroup(Group);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(Group.Id))
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

        private bool GroupExists(int id)
        {
            var group = _groupPageService.GetGroupById(id);
            return group != null;            
        }
    }
}
