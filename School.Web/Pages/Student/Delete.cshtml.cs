﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using School.Application.Models;
using School.Application.Services;

namespace School.Web.Pages.Student
{
    public class DeleteModel : PageModel
    {
        private readonly IService<StudentModel> _service;

        public DeleteModel(IService<StudentModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [BindProperty]
        public StudentModel Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            Student = await _service.GetById(studentId.Value);
            if (Student == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            await _service.Delete(Student);
            return RedirectToPage("./Index");
        }
    }
}
