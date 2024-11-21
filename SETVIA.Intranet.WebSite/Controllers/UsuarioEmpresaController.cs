using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SETVIA.Intranet.WebSite.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        // GET: UsuarioEmpresa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LisUsuarioEmpresa()
        {
            return View();
        }

        // GET: UsuarioEmpresa/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioEmpresa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioEmpresa/Create
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

        // GET: UsuarioEmpresa/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioEmpresa/Edit/5
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

        // GET: UsuarioEmpresa/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioEmpresa/Delete/5
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
