using System;
using Microsoft.AspNetCore.Mvc;
using projectBLL.interfaces;
using data_Access_layer.model;

namespace WebApplication8.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork iunitOfWork;

        public DepartmentController(IunitOfWork iunitOfWork)
        {
            this.iunitOfWork = iunitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["mess"] = "Hello from our department view data";
            ViewBag.message = "Hello from ViewBag";
            var departments = iunitOfWork.departmentRepo.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                iunitOfWork.departmentRepo.add(department);
                int res = iunitOfWork.Save();
                if (res > 0)
                {
                    TempData["message"] = $"Department created successfully: {department.name}";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Failed to create department.");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var department = iunitOfWork.departmentRepo.getbyid(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                iunitOfWork.departmentRepo.update(department);
                iunitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Details of Department  
        public IActionResult Details(int id)
        {
            var department = iunitOfWork.departmentRepo.getbyid(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var department = iunitOfWork.departmentRepo.getbyid(id);
            if (department == null)
            {
                return NotFound();
            }

            iunitOfWork.departmentRepo.delete(department);
            iunitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}