using AutoMapper;
using leave_management.Contracts;
using leave_management.Data;
using leave_management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Controllers
{
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeRepository _repo;
        private readonly IMapper _mapper;
        public LeaveTypeController(ILeaveTypeRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: LeaveTypeController
        public ActionResult Index()
        {
            var LeaveTypes = _repo.FindAll().ToList();
            var Model = _mapper.Map<List<LeaveType>, List<LeaveTypeVM>>(LeaveTypes);
          

            return View(Model);
        }

        // GET: LeaveTypeController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                if (!_repo.isExist(id))
                {

                    ModelState.AddModelError("", "data do not found");
                }
                var LeaveType = _repo.FindById(id);
               var Detail = _mapper.Map<LeaveTypeVM>(LeaveType);

                return View(Detail);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: LeaveTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LeaveTypeVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                leaveType.DateCreated = DateTime.Now;
                var create = _repo.Create(leaveType);
                if (!create)
                {
                   
                    ModelState.AddModelError("", "error encountored while adding to database");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "something went wrong");
                return View(model);
            }
        }

        // GET: LeaveTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();
            }
            var LeaveType = _repo.FindById(id);
            var model = _mapper.Map<LeaveTypeVM>(LeaveType); 

            return View(model);
        }

        // POST: LeaveTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LeaveTypeVM model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var leaveType = _mapper.Map<LeaveType>(model);
                var update = _repo.Update(leaveType);
                if (!update)
                {

                  ModelState.AddModelError("","something went wrong");
                    return View(model);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            if (!_repo.isExist(id))
            {
                return NotFound();

            }
            var val = _repo.FindById(id);
            var data = _repo.Delete(val);
            if (!data)
            {
                return View(data);

            }
            return RedirectToAction(nameof(Index));
        }

        //// POST: LeaveTypeController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, LeaveTypeVM model)
        //{
        //    try
        //    {
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
