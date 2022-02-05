using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Controllers
{
    public class CoursesController : Controller
    {
        private readonly IService<CourseModel> _service;

        public CoursesController(IService<CourseModel> service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        // GET: Courses
        public async Task<IActionResult> Index(string searchTerm, int courseId)
        {
            ViewData["CurrentFilter"] = searchTerm;
            IEnumerable<CourseModel> courseList;

            if (courseId != 0)
            {
                courseList = new List<CourseModel> { await _service.GetById(courseId) };
                return View(courseList);
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                courseList = await _service.GetAll();
                return View(courseList);
            }

            courseList = await _service.GetBySearch(searchTerm);
            return View(courseList);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var course = await _context.Courses
            //    .FirstOrDefaultAsync(c => c.CourseID == id);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            return View();
        }
    }
}
