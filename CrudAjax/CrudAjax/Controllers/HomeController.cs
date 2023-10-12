using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrudAjax.Models;

namespace CrudAjax.Controllers
{
    public class HomeController : Controller
    {
        EmployeeDB empDB = new EmployeeDB();         

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(empDB.Listall(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Create(Employee emp)
        {
            return Json(empDB.AddEmp(emp), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(Employee emp)
        {

            return Json(empDB.UpdateEmp(emp), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetById(int ID)
        {
            var gbi = empDB.Listall().Find(X => X.EmpId.Equals(ID));
            return Json(gbi, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int ID)
        {
            return Json(empDB.DeleteEmp(ID), JsonRequestBehavior.AllowGet);
        }
        
    }
}