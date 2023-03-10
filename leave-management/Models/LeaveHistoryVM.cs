using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveHistoryVM
    {
        public int Id { get; set; }


        public EmployeeVM RequestingEmployee { get; set; }

        public string RequestingEmployeeId { get; set; }

    
        public DateTime StartDate { get; set; }
      
        public DateTime EndDate { get; set; }


        public LeaveTypeVM LeaveType { get; set; }

        public int LeaveTypeId { get; set; }
        public IEnumerable<SelectListItem> LeaveTypes { get; set; }

        public DateTime Daterequested { get; set; }

        public DateTime DateActioned { get; set; }

        public bool? Approved { get; set; }

        public EmployeeVM ApprovedBy { get; set; }


        public String ApprovedById { get; set; }
    }
}
