using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Application.Models;
using School.Domain.Interfaces;
using System.Linq;

namespace School.Web.Pages.Group
{
    public class IndexModel : PageModel
    {
        private readonly IService<GroupModel> _service;

        public IndexModel(IService<GroupModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IEnumerable<GroupModel> GroupList { get; set; } = new List<GroupModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int groupId, int parentId)
        {
            if (groupId != 0)
            {
                GroupList = new List<GroupModel> { await _service.GetById(groupId) };
                return Page();
            }

            if (parentId != 0)
            {
                GroupList = await _service.GetByParent(parentId);
                return Page();
            }

            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                GroupList = await _service.GetAll();
                return Page();
            }

            GroupList = await _service.GetBySearch(SearchTerm);
            return Page();
        }
    }
}
