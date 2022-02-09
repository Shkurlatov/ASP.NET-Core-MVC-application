using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Details(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _service.GetById(courseId.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseModel course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            await _service.Create(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _service.GetById(courseId.Value);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseModel course)
        {
            if (!ModelState.IsValid)
            {
                return View(course);
            }

            await _service.Update(course);
            return RedirectToAction(nameof(Index));
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? courseId)
        {
            if (courseId == null)
            {
                return NotFound();
            }

            var course = await _service.GetById(courseId.Value);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CourseModel course)
        {
            await _service.Delete(course);
            return RedirectToAction(nameof(Index));
        }
    }
}
