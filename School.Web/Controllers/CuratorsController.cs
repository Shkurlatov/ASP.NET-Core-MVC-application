using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using School.Application.Models.Studies;
using School.Application.Models.Users;
using School.Domain.Interfaces.Studies;
using School.Domain.Interfaces.Users;

namespace School.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CuratorsController : Controller
    {
        private readonly IUserService<CuratorModel> _curatorService;
        private readonly IStudyService<GroupModel> _parentService;

        public CuratorsController(IUserService<CuratorModel> curatorService, IStudyService<GroupModel> parentService)
        {
            _curatorService = curatorService ?? throw new ArgumentNullException(nameof(curatorService));
            _parentService = parentService ?? throw new ArgumentNullException(nameof(parentService));
        }

        // GET: Curators
        public async Task<IActionResult> Index()
        {
            var curatorList = await _curatorService.GetAll();
            return View(curatorList);
        }

        // GET: Curators/Details/5
        public async Task<IActionResult> Details(string curatorId)
        {
            if (string.IsNullOrWhiteSpace(curatorId))
            {
                return NotFound();
            }

            var curator = await _curatorService.GetById(curatorId);
            if (curator == null)
            {
                return NotFound();
            }

            return View(curator);
        }


        // GET: Curators/Edit/5
        public async Task<IActionResult> Edit(string curatorId)
        {
            if (string.IsNullOrWhiteSpace(curatorId))
            {
                return NotFound();
            }

            var curator = await _curatorService.GetById(curatorId);
            if (curator == null)
            {
                return NotFound();
            }

            var allGroups = await _parentService.GetAll();
            var availableGroups = allGroups.Where(g => g.Curator == null || g.Id == curator.GroupId);
            ViewData["GroupId"] = new SelectList(availableGroups, "Id", "Name");
            return View(curator);
        }

        // POST: Curators/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CuratorModel curator)
        {
            if (!ModelState.IsValid)
            {
                return View(curator);
            }

            if (curator.GroupId == 0)
            {
                curator.GroupId = default;
            }

            await _curatorService.Update(curator);
            return RedirectToAction(nameof(Index));
        }

        // GET: Curators/Delete/5
        public async Task<IActionResult> Delete(string curatorId)
        {
            if (string.IsNullOrWhiteSpace(curatorId))
            {
                return NotFound();
            }

            var curator = await _curatorService.GetById(curatorId);
            if (curator == null)
            {
                return NotFound();
            }
            return View(curator);
        }

        // POST: Curators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(CuratorModel curator)
        {
            await _curatorService.Delete(curator);
            return RedirectToAction(nameof(Index));
        }
    }
}
