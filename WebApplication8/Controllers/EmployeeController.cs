using AutoMapper;
using data_Access_layer.model;
using Microsoft.AspNetCore.Mvc;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using WebApplication8.Models;
using WebApplication8.StaticFile;

public class EmployeeController : Controller
{
    private readonly IunitOfWork iunitOfWork;
    public IMapper Mapper { get; }

    public EmployeeController(IunitOfWork iunitOfWork, IMapper mapper)
    {
        this.iunitOfWork = iunitOfWork ?? throw new ArgumentNullException(nameof(iunitOfWork));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public IActionResult Index(string search)
    {
        IEnumerable<Employee> employees;

        if (string.IsNullOrEmpty(search))
        {
            employees = iunitOfWork.employeeRepo.GetAll();
        }
        else
        {
            employees = iunitOfWork.employeeRepo.SearchEmployees(search);
        }

        if (employees == null)
        {
            TempData["Message"] = "No employees found.";
            return View(new List<employeeViewModel>());
        }

        var mappedEmployees = Mapper.Map<IEnumerable<employeeViewModel>>(employees);
        return View(mappedEmployees);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.depart = iunitOfWork.departmentRepo.GetAll();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(employeeViewModel employee)
    {
        if (ModelState.IsValid)
        {
            employee.Image=Helper.uploadfile(employee.Imagefile, "images");
            var mappedEmployee = Mapper.Map<Employee>(employee);
            iunitOfWork.employeeRepo.add(mappedEmployee);
            var res = iunitOfWork.Save();
            if (res > 0)
            {
                TempData["Message"] = $"Employee successfully created: {employee.Name}";
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.depart = iunitOfWork.departmentRepo.GetAll();
        return View(employee);
    }

    public IActionResult Details(int id)
    {
        var employee = iunitOfWork.employeeRepo.getbyid(id);
        if (employee == null)
        {
            return NotFound();
        }
        var employeeViewModel = Mapper.Map<employeeViewModel>(employee);
        return View(employeeViewModel);
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        var employee = iunitOfWork.employeeRepo.getbyid(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        var mappedEmployee = Mapper.Map<employeeViewModel>(employee);
        ViewBag.depart = iunitOfWork.departmentRepo.GetAll();
        return View(mappedEmployee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(employeeViewModel employee)
    {
        if (ModelState.IsValid)
        {
            employee.Image = Helper.uploadfile(employee.Imagefile, "images");
            var mappedEmployee = Mapper.Map<Employee>(employee);
            iunitOfWork.employeeRepo.update(mappedEmployee);
              iunitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.depart = iunitOfWork.departmentRepo.GetAll();
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(employeeViewModel employeeView, int id)
    {
        // Retrieve the employee by ID  
        var employee = iunitOfWork.employeeRepo.getbyid(id);

        // Check if the employee exists  
        if (employee == null)
        {
            return NotFound();
        }

        var mappedEmployee = Mapper.Map<Employee>(employee);

        iunitOfWork.employeeRepo.delete(mappedEmployee);

        int res = iunitOfWork.Save();

        if (res > 0 && mappedEmployee.Image != null)
        {
            Helper.deletefile(mappedEmployee.Image, "images");
        }

        return RedirectToAction(nameof(Index));
    }
}