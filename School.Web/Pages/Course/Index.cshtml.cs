using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Web.Pages.Course
{
    public class IndexModel : PageModel
    {
        private readonly IService<CourseModel> _service;

        public IndexModel(IService<CourseModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IEnumerable<CourseModel> CourseList { get; set; } = new List<CourseModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            if (courseId != 0)
            {
                CourseList = new List<CourseModel> { await _service.GetById(courseId) };
                return Page();
            }

            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                CourseList = await _service.GetAll();
                return Page();
            }

            CourseList = await _service.GetBySearch(SearchTerm);
            return Page();
        }
    }
}