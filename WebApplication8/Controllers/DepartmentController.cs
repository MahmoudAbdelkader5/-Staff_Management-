using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using data_Access_layer.model;
using projectBLL.interfaces;
using projectBLL.repo;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication8.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;

        public DepartmentController(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["mess"] = "Hello from our department view data";
            ViewBag.message = "Hello from ViewBag";
            var departments = await _unitOfWork.departmentRepo.GetAllAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.departmentRepo.AddAsync(department);
                await _unitOfWork.Save();

                TempData["message"] = $"Department created successfully: {department.name}";
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var department = await _unitOfWork.departmentRepo.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.departmentRepo.UpdateAsync(department);
                await _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        public async Task<IActionResult> Details(int id)
        {
            var department = await _unitOfWork.departmentRepo.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var department = await _unitOfWork.departmentRepo.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            _unitOfWork.departmentRepo.DeleteAsync(department);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}