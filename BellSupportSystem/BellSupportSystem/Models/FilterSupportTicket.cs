using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BellSupportSystem.Models
{
    public class FilterSupportTicket
    {
        public List<SelectListItem> FilterByList { get; set; }
        public string FilterBy { get; set; }
        public string Text { get; set; }
    }
    
}