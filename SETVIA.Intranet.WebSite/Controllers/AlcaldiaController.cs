﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SETVIA.Intranet.WebSite.Controllers
{
    public class AlcaldiaController : Controller
    {
        // GET: Alcaldia
        public ActionResult NewEmpresa()
        {
            return View();
        }
        public ActionResult NewTarifario()
        {
            return View();
        }
        public ActionResult ListEmpresa()
        {
            return View();
        }
        public ActionResult ListVia()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: Alcaldia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Alcaldia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alcaldia/Create
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

        // GET: Alcaldia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Alcaldia/Edit/5
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

        // GET: Alcaldia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alcaldia/Delete/5
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
