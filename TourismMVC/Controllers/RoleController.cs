using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Core.Entities;
using TourismMVC.ViewModels;

namespace TourismMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<ApplicationRole> roleManager,IMapper mapper)
        {
            _roleManager = roleManager;
            this.mapper = mapper;
        }
        // GET: RoleController
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id= R.Id,
                    RoleName=R.Name,
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                 var mapoedrole= new RoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                };

                return View(new List<RoleViewModel> { mapoedrole });
            }
          
        }

        // GET: RoleController/Details/5
        public async Task<IActionResult> Details(int? id,string viewname ="Details")
        {
            if (id is null)
                return BadRequest();

            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role is null)
                return NotFound();

            var mappedrole=mapper.Map<ApplicationRole,RoleViewModel>(role);
            return View(viewname, mappedrole);
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedrole = mapper.Map<RoleViewModel, ApplicationRole>(roleViewModel);
                    await _roleManager.CreateAsync(mappedrole);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(roleViewModel);
           
        }

        // GET: RoleController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id,"Edit");
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, RoleViewModel roleViewModel)
        {
            if (id != roleViewModel.Id)
                return BadRequest();

            if (ModelState.IsValid)  //server side validation
            {
                try
                {
                   
                    var role = await _roleManager.FindByIdAsync(id.ToString());
                    role.Name = roleViewModel.RoleName;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }


            }
            return View(roleViewModel);
        }

        // GET: RoleController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            try
               {
                    var role = await _roleManager.FindByIdAsync(id.ToString());
                   
                    await _roleManager.DeleteAsync(role);
                    return RedirectToAction(nameof(Index));
                    
                    
               }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction("Error","Home");

            }




        }
    }
}
