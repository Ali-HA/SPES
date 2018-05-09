using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ACMWeb.Models;
using ACMWeb.ViewModels;

namespace ACMWeb.Controllers
{
    public class PIMappingsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        // GET: PIMappings
        public ActionResult Index()
        {
            var pIMappings = db.PIMappings.Include(p => p.Course).Include(p => p.GradeItem).Include(p => p.PI).Include(p => p.Semester);
            return View(pIMappings.ToList());
        }

        public ActionResult ScaleGradesto4() {
            //db.AssessmentGrades.SqlQuery("UPDATE AssessmentGrades set Grade4 = Grade where GradeS IS NULL");
            //db.AssessmentGrades.SqlQuery("UPDATE AssessmentGrades set Grade4 = Round(4*GradeS/100,0) where GradeS IS NOT NULL");
            db.Database.ExecuteSqlCommand("UPDATE AssessmentGrades set Grade4 = Grade where GradeS IS NULL");
            db.Database.ExecuteSqlCommand("UPDATE AssessmentGrades set Grade4 = CEILING(GradeS/25) where GradeS IS NOT NULL");
            db.SaveChanges();
            //db.AssessmentGrades.Where(x => x.GradeS.HasValue).ForEachAsync(w => w.Grade4 = Math.Round(4*w.GradeS.Value/100));
            //db.AssessmentGrades.Where(x => !x.GradeS.HasValue).ForEachAsync(w => w.Grade4 = w.Grade);
            
            
            db.SaveChanges();
            return RedirectToAction("PIResults");
        }
        // GET: PIMappings
        public ActionResult PIResults()
        {
            //var pIMappings = db.PIMappings.Include(p => p.Course).Include(p => p.GradeItem).Include(p => p.PI).Include(p => p.Semester);
            var pis = db.PIMappings.Select(x => new PIResultsVM { PI = x.PI.PILetter, desc = x.PI.PIDesc, PIno=x.PI.PINo,
                            semester =x.Semester.ShortName, SO=x.PI.LO.LearningOutcome,
                            CourseId = x.CourseID, SemesterId=x.SemesterID, gradeItemId = x.GradeItemID }).ToList();
            
            foreach (var pi in pis) {
                pi.Sections = db.Sections.Where(x => x.CourseID == pi.CourseId).Select(w => new SectionPIResultsVM { SectionId = w.SectionID, Section = w.Code }).ToList();
                foreach (var s in pi.Sections) {
                    s.rc1 = db.AssessmentGrades.Where(x => x.Enroll.Section.CourseID == pi.CourseId && x.GradeItemID == pi.gradeItemId && (x.Grade4 == 1 || (x.Grade.HasValue  && x.Grade4 == 0)) && x.Enroll.Section.SemesterID == pi.SemesterId && x.Enroll.SectionID == s.SectionId && x.Enroll.Status == true).Count();
                    s.rc2 = db.AssessmentGrades.Where(x => x.Enroll.Section.CourseID == pi.CourseId && x.GradeItemID == pi.gradeItemId && x.Grade4 == 2 && x.Enroll.Section.SemesterID == pi.SemesterId && x.Enroll.SectionID == s.SectionId && x.Enroll.Status == true).Count();
                    s.rc3 = db.AssessmentGrades.Where(x => x.Enroll.Section.CourseID == pi.CourseId && x.GradeItemID == pi.gradeItemId && x.Grade4 == 3 && x.Enroll.Section.SemesterID == pi.SemesterId && x.Enroll.SectionID == s.SectionId && x.Enroll.Status == true).Count();
                    s.rc4 = db.AssessmentGrades.Where(x => x.Enroll.Section.CourseID == pi.CourseId && x.GradeItemID == pi.gradeItemId && x.Grade4 == 4 && x.Enroll.Section.SemesterID == pi.SemesterId && x.Enroll.SectionID == s.SectionId && x.Enroll.Status == true).Count();
                }
                
            }
            return View(pis);
        }

        // GET: PIMappings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIMapping pIMapping = db.PIMappings.Find(id);
            if (pIMapping == null)
            {
                return HttpNotFound();
            }
            return View(pIMapping);
        }

        // GET: PIMappings/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code");
            ViewBag.GradeItemID = new SelectList(db.GradeItems.Select(x => new { GradeItemID = x.GradeItemID, Name = x.GradeDistribution.Name + " - " +x.Name  }), "GradeItemID", "Name");
            ViewBag.PIID = new SelectList(db.PIs, "PIID", "PILetter");
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName");
            return View();
        }

        // POST: PIMappings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PIMappingID,SemesterID,PIID,CourseID,GradeItemID,Active,Type,CType")] PIMapping pIMapping)
        {
            if (ModelState.IsValid)
            {
                db.PIMappings.Add(pIMapping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", pIMapping.CourseID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", pIMapping.GradeItemID);
            ViewBag.PIID = new SelectList(db.PIs, "PIID", "PILetter", pIMapping.PIID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", pIMapping.SemesterID);
            return View(pIMapping);
        }

        // GET: PIMappings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIMapping pIMapping = db.PIMappings.Find(id);
            if (pIMapping == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", pIMapping.CourseID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", pIMapping.GradeItemID);
            ViewBag.PIID = new SelectList(db.PIs, "PIID", "PILetter", pIMapping.PIID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", pIMapping.SemesterID);
            return View(pIMapping);
        }

        // POST: PIMappings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PIMappingID,SemesterID,PIID,CourseID,GradeItemID,Active,Type,CType")] PIMapping pIMapping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pIMapping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "Code", pIMapping.CourseID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", pIMapping.GradeItemID);
            ViewBag.PIID = new SelectList(db.PIs, "PIID", "PILetter", pIMapping.PIID);
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "ShortName", pIMapping.SemesterID);
            return View(pIMapping);
        }

        // GET: PIMappings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PIMapping pIMapping = db.PIMappings.Find(id);
            if (pIMapping == null)
            {
                return HttpNotFound();
            }
            return View(pIMapping);
        }

        // POST: PIMappings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PIMapping pIMapping = db.PIMappings.Find(id);
            db.PIMappings.Remove(pIMapping);
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
