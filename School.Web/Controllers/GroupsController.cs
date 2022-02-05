using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models;
using School.Application.Services;

namespace School.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IService<GroupModel> _service;
        private readonly IService<CourseModel> _parentService;

        public GroupsController(IService<GroupModel> service, IService<CourseModel> parentService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        // GET: Groups
        public async Task<IActionResult> Index(string searchTerm, int groupId, int parentId)
        {
            ViewData["CurrentFilter"] = searchTerm;
            IEnumerable<GroupModel> groupList;

            if (groupId != 0)
            {
                groupList = new List<GroupModel> { await _service.GetById(groupId) };
                return View(groupList);
            }

            if (parentId != 0)
            {
                groupList = await _service.GetByParent(parentId);
                return View(groupList);
            }

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                groupList = await _service.GetAll();
                return View(groupList);
            }

            groupList = await _service.GetBySearch(searchTerm);
            return View(groupList);
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            var group = await _service.GetById(groupId.Value);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // GET: Groups/Create
        public async Task<IActionResult> Create()
        {
            var courses = await _parentService.GetAll();
            ViewData["CourseId"] = new SelectList(courses, "Id", "Name");
            return View();
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GroupModel group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            await _service.Create(group);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            var group = await _service.GetById(groupId.Value);
            if (group == null)
            {
                return NotFound();
            }

            ViewData["CourseId"] = new SelectList(await _parentService.GetAll(), "Id", "Name");
            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GroupModel group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            await _service.Update(group);
            return RedirectToAction(nameof(Index));
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? groupId)
        {
            if (groupId == null)
            {
                return NotFound();
            }

            var group = await _service.GetById(groupId.Value);
            if (group == null)
            {
                return NotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(GroupModel group)
        {
            await _service.Delete(group);
            return RedirectToAction(nameof(Index));
        }
    }
}
