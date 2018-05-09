using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACMWeb.Models;
using ACMWeb.ViewModels;
using FileHelpers;

namespace ACMWeb.Controllers
{
    public class EnrollsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();


        //Upload Entrollement CSV file
        public ActionResult Upload()
        {
            //ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                TempData["fileName"] = path;
            }

            return RedirectToAction("Uploaded");
        }

        public ActionResult Uploaded()
        {
            if (TempData["fileName"] != null)
            {
                string fileName = (string)TempData["fileName"];
                var engine = new FileHelperEngine<EnrollVM>();
                var records = engine.ReadFile(fileName);
                foreach (var rec in records)
                {
                    var en = new Enroll() { EnrollmentDate=DateTime.Now, SectionID = rec.CRN, StudentID = rec.StudentId, Status = rec.Status };
                    db.Enrolls.Add(en);
                    db.SaveChanges();
                }

                return View(records);
            }
            return View();
        }

        // GET: Enrolls
        public ActionResult Index()
        {
            var enrolls = db.Enrolls.Include(e => e.LetterGrade).Include(e => e.Section).Include(e => e.Student);
            return View(enrolls.ToList());
        }

        // GET: Enrolls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll enroll = db.Enrolls.Find(id);
            if (enroll == null)
            {
                return HttpNotFound();
            }
            return View(enroll);
        }

        // GET: Enrolls/Create
        public ActionResult Create()
        {
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade");
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstMidName");
            return View();
        }

        // POST: Enrolls/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnrollID,SectionID,StudentID,LetterGradeID,EnrollmentDate,Status")] Enroll enroll)
        {
            if (ModelState.IsValid)
            {
                db.Enrolls.Add(enroll);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enroll.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enroll.SectionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstMidName", enroll.StudentID);
            return View(enroll);
        }

        // GET: Enrolls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll enroll = db.Enrolls.Find(id);
            if (enroll == null)
            {
                return HttpNotFound();
            }
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enroll.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enroll.SectionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstMidName", enroll.StudentID);
            return View(enroll);
        }

        // POST: Enrolls/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnrollID,SectionID,StudentID,LetterGradeID,EnrollmentDate,Status")] Enroll enroll)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enroll).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enroll.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enroll.SectionID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstMidName", enroll.StudentID);
            return View(enroll);
        }

        // GET: Enrolls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enroll enroll = db.Enrolls.Find(id);
            if (enroll == null)
            {
                return HttpNotFound();
            }
            return View(enroll);
        }

        // POST: Enrolls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enroll enroll = db.Enrolls.Find(id);
            db.Enrolls.Remove(enroll);
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
