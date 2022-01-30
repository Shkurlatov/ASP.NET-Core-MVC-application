using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Application.Models;
using School.Application.Services;

namespace School.Web.Pages.Group
{
    public class DeleteModel : PageModel
    {
        private readonly IService<GroupModel> _service;

        public DeleteModel(IService<GroupModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            await _service.Delete(Group);
            return RedirectToPage("./Index");
        }
    }
}
