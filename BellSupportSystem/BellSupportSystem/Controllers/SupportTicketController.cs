using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BellSupportSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace BellSupportSystem.Controllers
{
    public class SupportTicketController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["K7Database"].ConnectionString;
        // GET: SupportTicket
        public ActionResult Index()
        {
            SupportTickets supportTicket = new SupportTickets();
            List<Department> departmentList = new List<Department>();
            supportTicket.RequestedByList = new List<SelectListItem>();
            supportTicket.Departments = new List<SelectListItem>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Department", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var department = new Department();
                                department.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                                department.DepartmentName = (reader["DepartmentName"]).ToString();
                                supportTicket.Departments.Add(new SelectListItem { Text = department.DepartmentName, Value = department.DepartmentID.ToString() });

                            }
                        }
                    }

                }
                connection.Close();
            }
            return View(supportTicket);
        }

        [HttpPost]
        public ActionResult Index(SupportTickets supportTicket)
        {
            try
            {

                supportTicket.Departments = new List<SelectListItem>();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Department", connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    var department = new Department();
                                    department.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                                    department.DepartmentName = (reader["DepartmentName"]).ToString();
                                    supportTicket.Departments.Add(new SelectListItem { Text = department.DepartmentName, Value = department.DepartmentID.ToString() });

                                }
                            }
                        }

                    }
                    supportTicket.RequestedByList = new List<SelectListItem>();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee where departmentID=" + supportTicket.Department, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    var employee = new Employee();
                                    employee.DepartmentID = Convert.ToInt32(reader["DepartmentID"]);
                                    employee.EmployeeName = (reader["EmployeeName"]).ToString();
                                    employee.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                                    supportTicket.RequestedByList.Add(new SelectListItem { Text = employee.EmployeeName, Value = employee.EmployeeID.ToString() });

                                }
                            }
                        }

                    }

                }

                return View(supportTicket);

            }
            catch (Exception ex)
            {
                return View(supportTicket);
            }
        }
        // POST: SupportTicket/Save/

        [HttpPost]
        public ActionResult Save(SupportTickets supportTicket)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string format = "yyyy-MM-dd HH:mm:ss";
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT into SupportTickets (ProjectName,DateTimeReceived,Department,RequestedBy,Description) values(" +"'" +supportTicket.ProjectName+ "'" + "," + "'" +supportTicket.DateTimeReceived.ToString(format)+ "'" + "," + "'"+ supportTicket.Department+ "'" + "," + "'"+ supportTicket.RequestedBy+ "'" + "," + "'"+ supportTicket.Description+ "'" + ")", connection))
                {
                    int recordsAffected = cmd.ExecuteNonQuery();
                    if (recordsAffected != 0)
                    {
                        return Json(new { success = true, responseText = "Successfully Saved!" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                        return Json(new { success = false, responseText = "Application encountered an issue.. Please Check" }, JsonRequestBehavior.AllowGet);
                }

            }
        }

    }
}
