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
    public class GradeDistributionsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        // GET: GradeDistributions
        public ActionResult Index()
        {
            return View(db.GradeDistributions.ToList());
        }

        // GET: GradeDistributions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeDistribution gradeDistribution = db.GradeDistributions.Find(id);
            if (gradeDistribution == null)
            {
                return HttpNotFound();
            }
            return View(gradeDistribution);
        }

        // GET: GradeDistributions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GradeDistributions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeDistributionID,Name,EffectiveDate")] GradeDistribution gradeDistribution)
        {
            if (ModelState.IsValid)
            {
                db.GradeDistributions.Add(gradeDistribution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gradeDistribution);
        }

        // GET: GradeDistributions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeDistribution gradeDistribution = db.GradeDistributions.Find(id);
            if (gradeDistribution == null)
            {
                return HttpNotFound();
            }
            return View(gradeDistribution);
        }

        // POST: GradeDistributions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeDistributionID,Name,EffectiveDate")] GradeDistribution gradeDistribution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradeDistribution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gradeDistribution);
        }

        // GET: GradeDistributions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeDistribution gradeDistribution = db.GradeDistributions.Find(id);
            if (gradeDistribution == null)
            {
                return HttpNotFound();
            }
            return View(gradeDistribution);
        }

        // POST: GradeDistributions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradeDistribution gradeDistribution = db.GradeDistributions.Find(id);
            db.GradeDistributions.Remove(gradeDistribution);
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
