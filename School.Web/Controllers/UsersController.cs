using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School.Application.Models;
using School.Persistence.Data;

namespace School.Controllers
{
    public class UsersController : Controller
    {
        //private readonly SchoolContext _dbContext;

        //public UsersController(SchoolContext dbContext)
        //{
        //    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        //}

        //// GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<IdentityUser> userList;
        //    userList = await _dbContext.Set<IdentityUser>().AsNoTracking().ToListAsync();
        //    return View(userList);
        //}
    }
}
