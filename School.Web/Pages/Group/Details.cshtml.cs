using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.Interfaces;
using School.Web.ViewModels;

namespace School.Web.Pages.Group
{
    public class DetailsModel : PageModel
    {
        private readonly IGroupPageService _groupPageService;

        public DetailsModel(IGroupPageService groupPageService)
        {
            _groupPageService = groupPageService ?? throw new ArgumentNullException(nameof(groupPageService));
        }

        public GroupViewModel Group { get; set; }

        public async Task<IActionResult> OnGetAsync(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            //Group = await _groupPageService.GetGroupById(groupId.Value);
            if (Group == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
