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
    public class EnrollmentsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        //Upload Entrollement CSV file
        public ActionResult Upload()
        {
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

            return RedirectToAction("EnrollemntUp");
        }

        public ActionResult EnrollemntUp()
        {
            if (TempData["fileName"] != null)
            {
                string fileName = (string)TempData["fileName"];
                var engine = new FileHelperEngine<EnrollementVM>();
                var records = engine.ReadFile(fileName);
                List<Enrollment> enrols = new List<Enrollment>();
                foreach (var rec in records)
                {
                    if (db.Enrollments.Any(e => e.SectionID == rec.SectionID && e.StudentID == rec.StudentID))
                    {
                        var enrol1 = db.Enrollments.First(e => e.SectionID == rec.SectionID && e.StudentID == rec.StudentID);
                        db.Enrollments.Remove(enrol1);
                        db.SaveChanges();
                        rec.CourseCode = rec.CourseCode + "Duplicate";
                    }
                    var enrol = new Enrollment() { EnrollmentDate = DateTime.Now, SectionID = rec.SectionID, StudentID = rec.StudentID, Status = rec.Status };
                    db.Enrollments.Add(enrol);
                    db.SaveChanges();
                    //enrols.Add(enrol);
                }
                //db.Enrollments.AddRange(enrols);
                //db.SaveChanges();

                return View(records);
            }
            return View();
        }

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.LetterGrade).Include(e => e.Section).Include(e => e.Student);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade");
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code");
            ViewBag.Student_ID = new SelectList(db.Students, "StudentID", "FirstMidName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,Student_ID,LetterGradeID,EnrollmentDate,Status")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enrollment.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enrollment.SectionID);
            ViewBag.Student_ID = new SelectList(db.Students, "StudentID", "FirstMidName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enrollment.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enrollment.SectionID);
            ViewBag.Student_ID = new SelectList(db.Students, "StudentID", "FirstMidName", enrollment.StudentID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,Student_ID,LetterGradeID,EnrollmentDate,Status")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LetterGradeID = new SelectList(db.LetterGrades, "LetterGradeID", "Grade", enrollment.LetterGradeID);
            ViewBag.SectionID = new SelectList(db.Sections, "SectionID", "Code", enrollment.SectionID);
            ViewBag.Student_ID = new SelectList(db.Students, "StudentID", "FirstMidName", enrollment.StudentID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
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
