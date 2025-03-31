using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var mappedRoles = _mapper.Map<IEnumerable<roleViewModel>>(roles);
                return View(mappedRoles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(search);
                if (role != null)
                {
                    var mappedRole = _mapper.Map<roleViewModel>(role);
                    return View(new List<roleViewModel> { mappedRole });
                }
                return View(new List<roleViewModel>());
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(roleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var role = _mapper.Map<IdentityRole>(model);
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult Delete (string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                var result =_roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Details(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                var mappedRole = _mapper.Map<roleViewModel>(role);
                return View(mappedRole);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role != null)
            {
                var mappedRole = _mapper.Map<roleViewModel>(role);
                return View(mappedRole);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(roleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // First find the existing role
                var existingRole = await _roleManager.FindByIdAsync(model.Id);

                if (existingRole == null)
                {
                    return NotFound();
                }

                // Map the changes from the view model to the existing role
                _mapper.Map(model, existingRole);

                // Update the role
                var result = await _roleManager.UpdateAsync(existingRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
    }
}