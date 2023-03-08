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
    [Authorize(Roles ="Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveTypeRepository _leaverepo;
        private readonly ILeaveAllocationRepository _leaveallocationrepo;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        public LeaveAllocationController(
                    ILeaveTypeRepository leaverepo,
                    ILeaveAllocationRepository leaveallocationrepo,
                    IMapper mapper,
                    UserManager<IdentityUser> userManager

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
        public ActionResult Details(int id)
        {
            return View();
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
            return View();
        }

        // POST: LeaveAllocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
                if (_leaveallocationrepo.checkAlloaction(id, emp.Id))
                    continue;
                var allocation = new LeaveAllocationVM
                {
                    DateCreated = DateTime.Now,
                    EmployeeID = emp.Id,
                    LeaveTypeId = id,
                    NumberOfDays = Leavetype.DefaultDays,
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
