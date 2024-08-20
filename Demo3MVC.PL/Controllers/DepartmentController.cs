using AutoMapper;
using Demo3MVC.BLL.Interfaces;
using Demo3MVC.DAL.Models;
using Demo3MVC.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo3MVC.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments =await _unitofwork.DepartmentRepo.GetAllAsync();
            var mappedDepart = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(mappedDepart);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel deptVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDepart = _mapper.Map<DepartmentViewModel, Department>(deptVM);
                await _unitofwork.DepartmentRepo.AddAsync(mappedDepart);
                int Result =await _unitofwork.CompleteAsync();
                if (Result > 0)
                {
                    TempData["Message"] = "Department is Created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deptVM);
        }

        public async Task<IActionResult> Details(int? id , string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department =await _unitofwork.DepartmentRepo.GetByIdAsync(id.Value);
            if(department is null)
                return NotFound();
            var mappedDepart = _mapper.Map<Department,DepartmentViewModel>(department);
            return View(ViewName,mappedDepart);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id )
        {
            ///if (id is null)
            ///    return BadRequest();
            ///var department = _unitofwork.DepartmentRepo.GetById(id.Value);
            ///if (department is null)
            ///    return NotFound();
            ///return View(department);
            return await Details( id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel departVM , [FromRoute] int id)
        {
            if (id != departVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDepart = _mapper.Map<DepartmentViewModel,Department> (departVM);
                    _unitofwork.DepartmentRepo.Update(mappedDepart);
                    await _unitofwork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DepartmentViewModel departVM, [FromRoute] int id)
        {
            if (id != departVM.Id)
                return BadRequest();

            try
            {
                var mappedDepart = _mapper.Map<DepartmentViewModel, Department>(departVM);
                _unitofwork.DepartmentRepo.Delete(mappedDepart);
                await _unitofwork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }catch(System.Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(departVM);
            }
        }

    }
}
