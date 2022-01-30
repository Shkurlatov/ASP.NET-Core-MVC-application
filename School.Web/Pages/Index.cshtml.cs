using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Application.Models;
using School.Domain.Interfaces;
using System.Linq;

namespace School.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IService<StudentModel> _service;

        public IndexModel(IService<StudentModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public IEnumerable<StudentModel> StudentList { get; set; } = new List<StudentModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task<IActionResult> OnGetAsync(int studentId, int parentId)
        {
            if (studentId != 0)
            {
                StudentList.Append(await _service.GetById(studentId));
                return Page();
            }

            if (parentId != 0)
            {
                StudentList = await _service.GetByParent(parentId);
                return Page();
            }

            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                StudentList = await _service.GetAll();
                return Page();
            }

            StudentList = await _service.GetBySearch(SearchTerm);
            return Page();
        }
    }
}
