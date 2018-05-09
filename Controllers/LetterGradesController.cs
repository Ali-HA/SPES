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
    public class LetterGradesController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        // GET: LetterGrades
        public ActionResult Index()
        {
            return View(db.LetterGrades.ToList());
        }

        // GET: LetterGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LetterGrade letterGrade = db.LetterGrades.Find(id);
            if (letterGrade == null)
            {
                return HttpNotFound();
            }
            return View(letterGrade);
        }

        // GET: LetterGrades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LetterGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LetterGradeID,Grade,MinGrade,MaxGrade")] LetterGrade letterGrade)
        {
            if (ModelState.IsValid)
            {
                db.LetterGrades.Add(letterGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(letterGrade);
        }

        // GET: LetterGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LetterGrade letterGrade = db.LetterGrades.Find(id);
            if (letterGrade == null)
            {
                return HttpNotFound();
            }
            return View(letterGrade);
        }

        // POST: LetterGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LetterGradeID,Grade,MinGrade,MaxGrade")] LetterGrade letterGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(letterGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(letterGrade);
        }

        // GET: LetterGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LetterGrade letterGrade = db.LetterGrades.Find(id);
            if (letterGrade == null)
            {
                return HttpNotFound();
            }
            return View(letterGrade);
        }

        // POST: LetterGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LetterGrade letterGrade = db.LetterGrades.Find(id);
            db.LetterGrades.Remove(letterGrade);
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
