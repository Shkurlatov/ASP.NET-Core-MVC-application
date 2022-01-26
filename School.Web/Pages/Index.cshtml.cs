using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Web.Interfaces;
using School.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace School.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexPageService _indexPageService;

        public IndexModel(IIndexPageService indexPageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
        }

        //public IEnumerable<StudentViewModel> StudentList { get; set; } = new List<StudentViewModel>();
        //public GroupViewModel GroupModel { get; set; } = new GroupViewModel();
        //public CourseViewModel CourseModel { get; set; } = new CourseViewModel();

        public async Task<IActionResult> OnGet()
        {
            //StudentList = await _indexPageService.GetStudents();
            //GroupModel = await _indexPageService.GetGroupWithStudents(1);
            //CourseModel = await _indexPageService.GetCourseWithGroups(1);

            return Page();
        }
    }
}
