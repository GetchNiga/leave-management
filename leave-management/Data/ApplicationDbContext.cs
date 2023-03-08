using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using leave_management.Models;

namespace leave_management.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Employee>Emplooyes { get; set; }
        public DbSet<LeaveType> leaveTypes { get; set; }
        public DbSet<LeaveHistory> LeaveHistories { get; set; }
        public DbSet<LeaveAllocation>leaveAllocations { get; set; }
        public DbSet<leave_management.Models.LeaveTypeVM> LeaveTypeVM { get; set; }
        public DbSet<leave_management.Models.EmployeeVM> EmployeeVM { get; set; }
    }
}
