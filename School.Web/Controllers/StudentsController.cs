using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IService<StudentModel> _service;
        private readonly IService<GroupModel> _parentService;

        public StudentsController(IService<StudentModel> service, IService<GroupModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        // GET: Groups
        public async Task<IActionResult> Index(string searchTerm, int studentId, int parentId)
        {
            ViewData["CurrentFilter"] = searchTerm;
            IEnumerable<StudentModel> studentList;

            if (studentId != 0)
            {
                studentList = new List<StudentModel> { await _service.GetById(studentId) };
                return View(studentList);
            }

            if (parentId != 0)
            {
                studentList = await _service.GetByParent(parentId);
                return View(studentList);
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                studentList = await _service.GetAll();
                return View(studentList);
            }

            studentList = await _service.GetBySearch(searchTerm);
            return View(studentList);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            var student = await _service.GetById(studentId.Value);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            var groups = await _parentService.GetAll();
            ViewData["GroupId"] = new SelectList(groups, "Id", "Name");
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _service.Create(student);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            var student = await _service.GetById(studentId.Value);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["GroupId"] = new SelectList(await _parentService.GetAll(), "Id", "Name");
            return View(student);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentModel student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _service.Update(student);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? studentId)
        {
            if (studentId == null)
            {
                return NotFound();
            }

            var student = await _service.GetById(studentId.Value);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StudentModel student)
        {
            await _service.Delete(student);
            return RedirectToAction(nameof(Index));
        }
    }
}
