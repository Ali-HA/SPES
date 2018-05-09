using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACMWeb.Models;

namespace ACMWeb.Controllers
{
    public class LOesController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        // GET: LOes
        public ActionResult Index()
        {
            var lOs = db.LOs.Include(l => l.Department);
            return View(lOs.ToList());
        }

        // GET: LOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LO lO = db.LOs.Find(id);
            if (lO == null)
            {
                return HttpNotFound();
            }
            return View(lO);
        }

        // GET: LOes/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: LOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LOID,LearningOutcome,LODesc,DepartmentID")] LO lO)
        {
            if (ModelState.IsValid)
            {
                db.LOs.Add(lO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", lO.DepartmentID);
            return View(lO);
        }

        // GET: LOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LO lO = db.LOs.Find(id);
            if (lO == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", lO.DepartmentID);
            return View(lO);
        }

        // POST: LOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LOID,LearningOutcome,LODesc,DepartmentID")] LO lO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", lO.DepartmentID);
            return View(lO);
        }

        // GET: LOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LO lO = db.LOs.Find(id);
            if (lO == null)
            {
                return HttpNotFound();
            }
            return View(lO);
        }

        // POST: LOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LO lO = db.LOs.Find(id);
            db.LOs.Remove(lO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
