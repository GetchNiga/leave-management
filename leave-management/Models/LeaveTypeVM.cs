using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace leave_management.Models
{
    public class LeaveTypeVM
    {
        
        public int ID { get; set; }
        [Required]
        [Range(1,25,ErrorMessage="pleae enter valid number" )]    
        public int DefaultDays { get; set; }
        [Required]
        public string Name { get; set; }
       [Display(Name ="Date Created")]
        public DateTime? DateCreated { get; set; }



    }
}
