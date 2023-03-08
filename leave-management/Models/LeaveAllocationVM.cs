using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveAllocationVM
    {
        public int Id { get; set; }

     
        public int NumberOfDays { get; set; }

        public DateTime DateCreated { get; set; }

        public int Period { get; set; }
        public EmployeeVM Employee { get; set; }


        public string EmployeeID { get; set; }

        public LeaveTypeVM LeaveType { get; set; }


        public int LeaveTypeId { get; set; }

        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

    }

    public class CreateLeaveAllocationVM
    {

        public int NumberUpdated { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set; }

    }
    public class ViewAllocationVm
    {
        public EmployeeVM Employee { get; set; }
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> leaveAllocations { get; set; }

    }
}
