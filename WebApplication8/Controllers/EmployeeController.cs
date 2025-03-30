using AutoMapper;
using data_Access_layer.model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectBLL.interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication8.Models;
using WebApplication8.StaticFile;
[Authorize]

public class EmployeeController : Controller
{
    private readonly IunitOfWork _unitOfWork;
    public IMapper Mapper { get; }

    public EmployeeController(IunitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IActionResult> Index(string search)
    {
        IEnumerable<Employee> employees;

        if (string.IsNullOrEmpty(search))
        {
            employees = await _unitOfWork.employeeRepo.GetAllAsync();
        }
        else
        {
            employees =  _unitOfWork.employeeRepo.SearchEmployees(search);  
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
    public async Task<IActionResult> Create()
    {
        ViewBag.depart = await _unitOfWork.departmentRepo.GetAllAsync(); 
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(employeeViewModel employee)
    {
        if (ModelState.IsValid)
        {
            employee.Image = Helper.uploadfile(employee.Imagefile, "images");
            var mappedEmployee = Mapper.Map<Employee>(employee);
            await _unitOfWork.employeeRepo.AddAsync(mappedEmployee); // Await async method  
            var res = await _unitOfWork.Save();  

            if (res > 0)
            {
                TempData["Message"] = $"Employee successfully created: {employee.Name}";
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.depart = await _unitOfWork.departmentRepo.GetAllAsync(); 
        return View(employee);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _unitOfWork.employeeRepo.GetByIdAsync(id); // Ensure this is awaited  
        if (employee == null)
        {
            return NotFound();
        }
        var employeeViewModel = Mapper.Map<employeeViewModel>(employee);
        return View(employeeViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest();
        }

        var employee = await _unitOfWork.employeeRepo.GetByIdAsync(id.Value); // Ensure this is awaited  
        if (employee == null)
        {
            return NotFound();
        }

        var mappedEmployee = Mapper.Map<employeeViewModel>(employee);
        ViewBag.depart = await _unitOfWork.departmentRepo.GetAllAsync(); 
        return View(mappedEmployee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(employeeViewModel employee)
    {
        if (ModelState.IsValid)
        {
            employee.Image = Helper.uploadfile(employee.Imagefile, "images");
            var mappedEmployee = Mapper.Map<Employee>(employee);
             _unitOfWork.employeeRepo.UpdateAsync(mappedEmployee); // Await async method  
            await _unitOfWork.Save(); // Ensure SaveAsync is awaited  
            return RedirectToAction(nameof(Index));
        }

        ViewBag.depart = await _unitOfWork.departmentRepo.GetAllAsync(); // Make sure to await async call  
        return View(employee);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _unitOfWork.employeeRepo.GetByIdAsync(id); // Ensure this is awaited  
        if (employee == null)
        {
            return NotFound();
        }

        var mappedEmployee = Mapper.Map<Employee>(employee);
         _unitOfWork.employeeRepo.DeleteAsync(mappedEmployee); // Await async method  
        var res = await _unitOfWork.Save(); // Ensure SaveAsync is awaited  

        if (res > 0 && mappedEmployee.Image != null)
        {
            Helper.deletefile(mappedEmployee.Image, "images");
        }

        return RedirectToAction(nameof(Index));
    }
}