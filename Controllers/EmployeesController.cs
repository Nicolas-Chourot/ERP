using ERP.Models;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class EmployeesController : Controller
    {
        ERPDBEntities DB = new ERPDBEntities();

        public ActionResult Index()
        {
            return View(DB.Employees);
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
    }
}