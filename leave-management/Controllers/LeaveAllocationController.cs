using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    //[Authorize(Roles ="Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaverepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<Employee> _userManager;
        public LeaveAllocationController(
                    ILeaveTypeRepository leaverepo,
                    ILeaveAllocationRepository leaveallocationrepo,
                    IMapper mapper,
                    UserManager<Employee> userManager

                )
        {
            _leaverepo = leaverepo;
            _leaveallocationrepo = leaveallocationrepo;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: LeaveAllocationController
        public ActionResult Index()
        {
            var LeaveTypes = _leaverepo.FindAll().ToList();
            var Model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(LeaveTypes);


            return View(Model);
        }

        // GET: LeaveAllocationController/Details/5
        public ActionResult Details(string id)
        {
            var Employee = _mapper.Map<EmployeeVM>(_userManager.FindByIdAsync(id).Result);
            var period = DateTime.Now.Year;
            
            var Allocation =_mapper.Map<List<LeaveAllocationVM>>(_leaveallocationrepo.GetLeaveAllocationByEmployee(id));
            var model = new ViewAllocationVm
            {
                Employee = Employee,
                leaveAllocations=Allocation
            };
            return View(model);
        }

        // GET: LeaveAllocationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocationController/Edit/5
        public ActionResult Edit(int id)
        {
            var leaveAllocation = _leaverepo.FindById(id);
            var Model = _mapper.Map<EditLeaveAllocationVM>(leaveAllocation);
            return View(Model);
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationVM Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(Model);
                }
                var Record = _leaveallocationrepo.FindById(Model.Id);
                Record.NumberOfDays = Model.NumberOfDays;
                var Allocation = _mapper.Map<LeaveAllocation>(Model);
                var Issucces = _leaveallocationrepo.Update(Allocation);
                if (!Issucces)
                {
                    ModelState.AddModelError("", "Error while saving");

                }
                return RedirectToAction(nameof(Details),new { id =Model.Employee});
            }
            catch
            {
                return View(Model);
            }
        }

        // GET: LeaveAllocationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Setleave(int id)
        {
            var Leavetype = _leaverepo.FindById(id);
            var Employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            foreach (var emp in Employees)
            {
                if (_leaveallocationrepo.checkAlloaction(id, emp.TaxId))
                    continue;
                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeID = emp.TaxId,
                    LeaveTypeId = id,
                    //NumberOfDays = Leavetype.DefaultDays,
                    Period = DateTime.Now.Year

                };
                var LeaveAllocation = _mapper.Map<LeaveAllocation>(allocation);
                _leaveallocationrepo.Create(LeaveAllocation);
            }
            return RedirectToAction(nameof(Index));  
        }

        public ActionResult ListEmployees()
        {
            var Employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model =_mapper.Map<List<EmployeeVM>>(Employees);
            return View(model);
        }
    }
}
