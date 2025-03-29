using System;
using Microsoft.AspNetCore.Mvc;
using projectBLL.interfaces;
using data_Access_layer.model;

namespace WebApplication8.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo _dep;

        public DepartmentController(IDepartmentRepo dep)
        {
            _dep = dep;
        }

        public ActionResult Index()
        {
            ViewData["mess"] = "Hallo from our department view data";
            ViewBag.message = "hallo view bag";
            var departments = _dep.GetAll();
            return View(departments);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token validation for security
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                int res=_dep.add(department);
                if(res>0)
                {
                    TempData["message"] = $"department is created successed : {department.name}";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

      

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var departments = _dep.GetAll();

            if (id == null)
            {
                return BadRequest();
            }

            var department = _dep.getbyid(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _dep.update(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Add anti-forgery token validation for security
        public ActionResult Delete(int id) // Use ID instead of the entire object for deletion
        {
            var department = _dep.getbyid(id);
            if (department == null)
            {
                return NotFound();
            }

            _dep.delete(department);
            return RedirectToAction(nameof(Index));
        }
    }
}