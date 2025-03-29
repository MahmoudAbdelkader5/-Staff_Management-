using data_Access_layer.model;
using Microsoft.AspNetCore.Mvc;
using projectBLL.interfaces;
using System.Threading.Tasks;

public class EmployeeController : Controller
{
    private readonly IemployeeRepo _emp;
    private readonly IDepartmentRepo _department;

    public EmployeeController(IemployeeRepo emp, IDepartmentRepo department)
    {
        _emp = emp;
        _department = department;
    }

    public IActionResult Index()
    {
        var employees = _emp.GetAll();
        return View(employees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.depart = _department.GetAll(); // Corrected ViewBag key
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Employee employee) // Made method async
    {
        if (ModelState.IsValid)
        {
            int res =  _emp.add(employee); // Assuming AddAsync is available
            if (res > 0)
            {
                TempData["Message"] = $"Employee successfully created: {employee.Name}"; // Corrected TempData key
            }
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public  IActionResult Delete(int id) // Made method async
    {
        var employee =  _emp.getbyid(id); // Assuming GetByIdAsync is available
        if (employee == null)
        {
            return NotFound();
        }

         _emp.delete(employee); // Assuming DeleteAsync is available
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int? id) // Made method async
    {
        if (id == null)
        {
            return BadRequest();
        }

        var employee =  _emp.getbyid(id.Value); 
        if (employee == null)
        {
            return NotFound();
        }

        ViewBag.depart = _department.GetAll(); 
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public  IActionResult Edit(Employee employee) // Made method async
    {
        if (ModelState.IsValid)
        {
             _emp.update(employee); // Assuming UpdateAsync is available
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Departments = _department.GetAll(); // Re-populate ViewBag for validation errors
        return View(employee);
    }
}