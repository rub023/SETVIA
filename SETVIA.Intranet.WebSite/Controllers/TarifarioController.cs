using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SETVIA.Intranet.WebSite.Controllers
{
    public class TarifarioController : Controller
    {
        // GET: Tarifario
        public ActionResult NewTarifario()
        {
            return View();
        }
        public ActionResult ListTarifario()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: Tarifario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tarifario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tarifario/Create
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

        // GET: Tarifario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tarifario/Edit/5
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

        // GET: Tarifario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tarifario/Delete/5
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
