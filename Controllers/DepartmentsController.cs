using ERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class DepartmentsController : Controller
    {
        ERPDBEntities DB = new ERPDBEntities();
        public ActionResult Index()
        {
            return View(DB.Departments.OrderBy(d => d.Name));
        }
        public ActionResult Create()
        {
            return View(new Department());
        }
        [HttpPost]
        public ActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                DB.AddDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public ActionResult Edit(int id)
        {
            Department department = DB.Departments.Find(id);
            if (department != null)
            {
                return View(department);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                DB.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public ActionResult Delete(int id)
        {
            DB.DeleteDepartment(id);
            return RedirectToAction("Index");
        }
    }
}
