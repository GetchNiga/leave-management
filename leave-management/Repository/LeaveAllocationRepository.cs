using leave_management.Contracts;
using leave_management.Data;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveAllocationRepository : ILeaveAllocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveAllocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool checkAlloaction(int leavetypeid, string Employeeid)
        {

            var period = DateTime.Now.Year;
            return FindAll().Where(q=>q.EmployeeId== Employeeid && q.LeaveTypeident==leavetypeid && q.Period==period ).Any();
        }

        public bool Create(LeaveAllocation entity)
        {
            _db.leaveAllocations.Add(entity);
            return Save();

        }

        public bool Delete(LeaveAllocation entity)
        {
            _db.leaveAllocations.Remove(entity);
            return Save();
        }

        public ICollection<LeaveAllocation> FindAll()
        {
            return _db.leaveAllocations.ToList();

        }

        public LeaveAllocation FindById(int id)
        {
            return _db.leaveAllocations.Find(id);
        }

        public bool isExist(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            var x = _db.SaveChanges();
            return x > 0;

        }

        public bool Update(LeaveAllocation entity)
        {
            _db.leaveAllocations.Update(entity);
            return Save();
        }
        
    }
}
