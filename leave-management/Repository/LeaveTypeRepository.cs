using leave_management.Contracts;
using leave_management.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Repository
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool Create(LeaveType entity)
        {
            _db.leaveTypes.Add(entity);
            return Save();
            
        }

        public bool Delete(LeaveType entity)
        {
            _db.leaveTypes.Remove(entity);
            return Save();
        }

        public ICollection<LeaveType> FindAll()
        {
           return  _db.leaveTypes.ToList();
        
        }

        public LeaveType FindById(int id)
        {
            return _db.leaveTypes.Find(id);
        }

        public ICollection<LeaveType> GetEmployeesByLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
          var x=  _db.SaveChanges();
            return x > 0;

        }
        public bool isExist(int id)
        {
            var Exists = _db.leaveTypes.Any(q=>q.ID == id); 
            return Exists;
        }
        public bool Update(LeaveType entity)
        {
            _db.leaveTypes.Update(entity);
           return Save();
        }
    }
}
