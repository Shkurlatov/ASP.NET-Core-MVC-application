using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Group
{
    public class DeleteModel : PageModel
    {
        private readonly IGroupPageService _groupPageService;

        public DeleteModel(IGroupPageService groupPageService, IStudentPageService studentPageService)
        {
            _groupPageService = groupPageService ?? throw new ArgumentNullException(nameof(groupPageService));
        }
        public IEnumerable<StudentViewModel> StudentList { get; set; } = new List<StudentViewModel>();

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            await _groupPageService.DeleteGroup(Group);
            return RedirectToPage("./Index");
        }
    }
}
