using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.ViewModels;
using School.Web.Interfaces;
using System.Linq;

namespace School.Web.Pages.Group
{
    public class IndexModel : PageModel
    {
        private readonly IGroupPageService _groupPageService;

        public IndexModel(IGroupPageService groupPageService)
        {
            _groupPageService = groupPageService ?? throw new ArgumentNullException(nameof(groupPageService));
        }

        public IEnumerable<GroupViewModel> GroupList { get; set; } = new List<GroupViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int groupId, int courseId)
        {
            //if (courseId != 0)
            //{
            //    GroupList = await _groupPageService.GetGroupByCourse(courseId);
            //    return Page();
            //}

            //GroupList = await _groupPageService.GetGroups(SearchTerm, groupId);
            return Page();
        }
    }
}
