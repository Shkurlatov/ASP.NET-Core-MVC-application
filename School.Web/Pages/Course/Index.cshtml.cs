using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Web.Interfaces;
using School.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace School.Web.Pages.Course
{
    public class IndexModel : PageModel
    {
        private readonly ICoursePageService _coursePageService;

        public IndexModel(ICoursePageService coursePageService)
        {
            _coursePageService = coursePageService ?? throw new ArgumentNullException(nameof(coursePageService));
        }

        public IEnumerable<CourseViewModel> CourseList { get; set; } = new List<CourseViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            CourseList = await _coursePageService.GetCourses(SearchTerm, courseId);
            return Page();
        }
    }
}