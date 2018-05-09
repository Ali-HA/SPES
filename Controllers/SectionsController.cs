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
    public class SectionsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        // GET: Sections
        public ActionResult Index()
        {
            var sections = db.Sections.Include(s => s.ClassInstructor).Include(s => s.Course).Include(s => s.GradeDistribution).Include(s => s.LabInstructor).Include(s => s.Semester);
            return View(sections.ToList());
        }

        // GET: Sections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // GET: Sections/Create
        public ActionResult Create()
        {
            ViewBag.ClassInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code");
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name");
            ViewBag.LabInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName");
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName");
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SectionID,Code,CourseID,SemesterID,Room,ClassInstructorID,LabInstructorID,GradeDistributionID")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Sections.Add(section);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.ClassInstructorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", section.CourseID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", section.GradeDistributionID);
            ViewBag.LabInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.LabInstructorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", section.SemesterID);
            return View(section);
        }

        // GET: Sections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.ClassInstructorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", section.CourseID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", section.GradeDistributionID);
            ViewBag.LabInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.LabInstructorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", section.SemesterID);
            return View(section);
        }

        // POST: Sections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SectionID,Code,CourseID,SemesterID,Room,ClassInstructorID,LabInstructorID,GradeDistributionID")] Section section)
        {
            if (ModelState.IsValid)
            {
                db.Entry(section).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.ClassInstructorID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", section.CourseID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", section.GradeDistributionID);
            ViewBag.LabInstructorID = new SelectList(db.Instructors, "InstructorID", "FirstMidName", section.LabInstructorID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", section.SemesterID);
            return View(section);
        }

        // GET: Sections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section section = db.Sections.Find(id);
            if (section == null)
            {
                return HttpNotFound();
            }
            return View(section);
        }

        // POST: Sections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Section section = db.Sections.Find(id);
            db.Sections.Remove(section);
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
