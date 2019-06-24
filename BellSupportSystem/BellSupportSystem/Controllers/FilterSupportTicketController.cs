using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BellSupportSystem.Models;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data.Entity;

namespace BellSupportSystem.Controllers
{
    public class FilterSupportTicketController : Controller
    {
        // GET: FilterSupportTicket
        public ActionResult Index()
        {
            FilterSupportTicket filterSupportTicket = new FilterSupportTicket();
            return View(filterSupportTicket);
        }
        [HttpPost]
        public ActionResult Index(FilterSupportTicket filterSupportTicket)
        {

            return RedirectToAction("ShowData", filterSupportTicket);
        }

        public ActionResult ShowData(FilterSupportTicket filterSupportTicket)

        {
            K7Entities entities = new K7Entities();
            
            if(filterSupportTicket.FilterBy== "ProjectName")
            return View(entities.SupportTickets.ToList().Where(x=>x.ProjectName.Equals(filterSupportTicket.Text)));
            else if (filterSupportTicket.FilterBy == "DateTimeReceived")
                return View(entities.SupportTickets.ToList().Where(x => x.DateTimeReceived.Equals(filterSupportTicket.Text)));
            else if (filterSupportTicket.FilterBy == "Department")
                return View(entities.SupportTickets.ToList().Where(x => x.Department.Equals(filterSupportTicket.Text)));
            else if (filterSupportTicket.FilterBy == "RequestedBy")
                return View(entities.SupportTickets.ToList().Where(x => x.RequestedBy.Equals(filterSupportTicket.Text)));
            else if (filterSupportTicket.FilterBy == "Description")
                return View(entities.SupportTickets.ToList().Where(x => x.Description.Equals(filterSupportTicket.Text)));
            return View();
        }
    }
}
