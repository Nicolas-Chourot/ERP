using ERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class DepartmentsController : Controller
    {
        ERPDBEntities DB = new ERPDBEntities();
        // GET: Departments
        public ActionResult Index()
        {
            return View(DB.Departments.OrderBy(d => d.Name));
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View(new Department());
        }

        // POST: Departments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Departments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Departments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
