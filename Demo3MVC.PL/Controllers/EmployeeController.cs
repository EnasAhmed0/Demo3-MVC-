using AutoMapper;
using Demo3MVC.BLL.Interfaces;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.Helpers;
using Demo3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo3MVC.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        //Ask Clr to Inject an Object from class implement the interface => IUnitOfWork 
        //you must go to Allow Dependency injection into Configure servies to tell the clr how to create this object 
        public EmployeeController(IUnitOfWork unitofwork , IMapper mapper) 
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
            {
             employees =await _unitofwork.EmployeeRepo.GetAllAsync();
            //ViewData["Message"] = "Hello From View Data";
            //ViewBag.Message = "Hello From View Bag";
            }
            else
            {
                employees = _unitofwork.EmployeeRepo.GetEmployeeByName(SearchValue);
            }
            var mappedEmp = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewBag.Departments = _departRepo.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid)
            {
               string FileName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                employeeVM.ImageName = FileName;
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
               await _unitofwork.EmployeeRepo.AddAsync(mappedEmp);
                int Result = await _unitofwork.CompleteAsync();

                if (Result > 0)
                {
                    TempData["Message"] = "Employee is Added";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);

        }

        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var emp =await _unitofwork.EmployeeRepo.GetByIdAsync(id.Value);
            if(emp == null)
                return NotFound();
            var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);
            return View(viewName,mappedEmp);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //ViewBag.Departments = _departRepo.GetAll();
            return await Details(id.Value, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel empVM,[FromRoute]int id)
        {
            if(empVM.Id != id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    string ImageName = DocumentSettings.UploadFile(empVM.Image, "Images");
                    empVM.ImageName = ImageName;
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(empVM);
                    _unitofwork.EmployeeRepo.Update(mappedEmp);
                    await _unitofwork.CompleteAsync();
                

                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty,ex.Message);
                }
            }
            return View(empVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id.Value, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel empVM, [FromRoute] int id)
        {
            if (empVM.Id != id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel,Employee>(empVM);
                    _unitofwork.EmployeeRepo.Delete(mappedEmp);
                    var Result =await _unitofwork.CompleteAsync();
                    if (Result > 0 && empVM.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(empVM.ImageName, "Images");
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(empVM);
        }
    }
}
