using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BellSupportSystem.Models
{
    public class SupportTickets
    {
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public DateTime DateTimeReceived { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public List<SelectListItem> Departments { get; set; }
        [Required]
        public string RequestedBy { get; set; }
        [Required]
        public List<SelectListItem> RequestedByList { get; set; }
        [Required]
        public string Description { get; set; }
    }
}