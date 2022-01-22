using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Web.ViewModels;
using School.Web.Interfaces;

namespace School.Web.Pages.Student
{
    public class IndexModel : PageModel
    {
        private readonly IStudentPageService _studentPageService;

        public IndexModel(IStudentPageService studentPageService)
        {
            _studentPageService = studentPageService ?? throw new ArgumentNullException(nameof(studentPageService));
        }

        public IEnumerable<StudentViewModel> StudentList { get; set; } = new List<StudentViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int groupId)
        {
            if (groupId != 0)
            {
                StudentList = await _studentPageService.GetStudentByGroup(groupId);
                return Page();
            }

            StudentList = await _studentPageService.GetStudents(SearchTerm);
            return Page();
        }
    }
}
