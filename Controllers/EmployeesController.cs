using ERP.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class EmployeesController : Controller
    {
        ERPDBEntities DB = new ERPDBEntities();

        private string SortField
        {
            get
            {
                if (Session["SortField"] == null)
                    Session["SortField"] = "LastName";
                return (string)Session["SortField"];
            }
            set
            {
                Session["SortField"] = value;
            }
        }

        private bool SortDirection
        {
            get
            {
                if (Session["SortDirection"] == null)
                    Session["SortDirection"] = true;
                return (bool)Session["SortDirection"];
            }
            set
            {
                Session["SortDirection"] = value;
            }
        }

        private IEnumerable<Employee> GetSortedEmployees()
        {
            IEnumerable<Employee> employees = null;
            if (SortField != null)
            {
                switch (SortField)
                {
                    case "LastName":
                        if (SortDirection)
                            employees = DB.Employees.OrderBy(e => e.LastName).ThenBy(e => e.FirstName);
                        else
                            employees = DB.Employees.OrderByDescending(e => e.LastName).ThenByDescending(e => e.FirstName);
                        break;
                    case "BirthDate":
                        if (SortDirection)
                            employees = DB.Employees.OrderBy(e => e.BirthDate);
                        else
                            employees = DB.Employees.OrderByDescending(e => e.BirthDate);
                        break;
                    case "Department":
                        if (SortDirection)
                            employees = DB.Employees.OrderBy(e => e.Department.Name);
                        else
                            employees = DB.Employees.OrderByDescending(e => e.Department.Name);
                        break;
                    case "Classification":
                        if (SortDirection)
                            employees = DB.Employees.OrderBy(e => e.Classification.Name);
                        else
                            employees = DB.Employees.OrderByDescending(e => e.Classification.Name);
                        break;
                    default:
                        employees = DB.Employees;
                        break;
                }
            }
            return employees;
        }

        public ActionResult SetSortField(string sortfield)
        {
            if (sortfield == SortField)
                SortDirection = !SortDirection;
            else
                SortDirection = true;
            SortField = sortfield;
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            ViewBag.SortField = SortField;
            ViewBag.SortDirection = SortDirection;
            return View(GetSortedEmployees());
        }

        [HttpPost]
        public JsonResult EmailAvailable(string Email, int Id)
        {
            return Json(DB.EmailAvailable(Email, Id));
        }

        public PartialViewResult EmployeeForm(Employee employee)
        {
            return PartialView(employee);
        }

        public ActionResult Create()
        {
            ViewBag.Departments = DB.DepartmentsToSelectList("Département");
            ViewBag.Classifications = DB.ClassificationsToSelectList();
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                DB.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = DB.DepartmentsToSelectList("Département");
            ViewBag.Classifications = DB.ClassificationsToSelectList();
            return View(employee);
        }
        public ActionResult Details(int id)
        {
            Employee employee = DB.Employees.Find(id);
            if (employee != null)
            {
                return View(employee);
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var employee = DB.Employees.Find(id);
            if (employee != null)
            {
                ViewBag.Departments = DB.DepartmentsToSelectList();
                ViewBag.Classifications = DB.ClassificationsToSelectList();
                return View(employee);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                DB.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = DB.DepartmentsToSelectList();
            ViewBag.Classifications = DB.ClassificationsToSelectList();
            return View(employee);
        }
        public ActionResult Delete(int id)
        {
            DB.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}