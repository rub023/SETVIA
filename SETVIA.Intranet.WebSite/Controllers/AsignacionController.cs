using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SETVIA.Intranet.WebSite.Controllers
{
    public class AsignacionController : Controller
    {
        // GET: Vias
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListAsignacion()
        {
            return View();
        }
        // GET: Vias/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vias/Create
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

        // GET: Vias/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Vias/Edit/5
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

        // GET: Vias/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vias/Delete/5
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
