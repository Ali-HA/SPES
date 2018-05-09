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
using PagedList;

namespace ACMWeb.Controllers
{
    public class AssessmentGradesController : Controller
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
                var engine = new FileHelperEngine<GradeVM>();
                var records = engine.ReadFile(fileName);
                int EnrId;
                int GradeDistId;
                int grItem;
                foreach (var rec in records)
                {
                    //EnrId = db.Enrolls.Where(x => x.SectionID == rec.CRN && x.StudentID == rec.StudentId).FirstOrDefault().EnrollID;
                    var enr = db.Enrolls.Where(x => x.SectionID == rec.CRN && x.StudentID == rec.StudentId).FirstOrDefault();
                    //GradeDistId = db.Sections.Where(x => x.SectionID == rec.CRN).FirstOrDefault().GradeDistributionID;
                    GradeDistId = enr.Section.GradeDistributionID;
                    var Item = db.GradeItems.Where(x => x.Name == rec.Code && x.GradeDistributionID == GradeDistId).FirstOrDefault();
                    if (Item == null) { continue; }
                    grItem = Item.GradeItemID;
                    if (db.AssessmentGrades.Any(x => x.GradeItemID == grItem && x.EnrollID == enr.EnrollID)) { continue; }
                    var gr = new AssessmentGrade() { EnrollID = enr.EnrollID , Grade = rec.Grade, GradeItemID= grItem, GradeS=rec.GradeS };
                    db.AssessmentGrades.Add(gr);
                    db.SaveChanges();
                }

                return View(records);
            }
            return View();
        }

        // GET: AssessmentGrades
        public ActionResult Index(int? page)
        {
            var assessmentGrades = db.AssessmentGrades.Include(a => a.Enroll).Include(a => a.GradeItem);

            assessmentGrades = assessmentGrades.OrderBy(x => x.EnrollID).ThenBy(b => b.GradeItemID);
            int pageSize = 40;
            int pageNumber = (page ?? 1);

            return View(assessmentGrades.ToPagedList(pageNumber, pageSize));

            //return View(assessmentGrades.ToList());
        }

        // GET: AssessmentGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentGrade assessmentGrade = db.AssessmentGrades.Find(id);
            if (assessmentGrade == null)
            {
                return HttpNotFound();
            }
            return View(assessmentGrade);
        }

        // GET: AssessmentGrades/Create
        public ActionResult Create()
        {
            ViewBag.EnrollID = new SelectList(db.Enrolls, "EnrollID", "EnrollID");
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name");
            return View();
        }

        // POST: AssessmentGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssessmentGradeID,Grade,Grade4,GradeP,GradeS,GradeItemID,EnrollID")] AssessmentGrade assessmentGrade)
        {
            if (ModelState.IsValid)
            {
                db.AssessmentGrades.Add(assessmentGrade);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnrollID = new SelectList(db.Enrolls, "EnrollID", "EnrollID", assessmentGrade.EnrollID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", assessmentGrade.GradeItemID);
            return View(assessmentGrade);
        }

        // GET: AssessmentGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentGrade assessmentGrade = db.AssessmentGrades.Find(id);
            if (assessmentGrade == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnrollID = new SelectList(db.Enrolls, "EnrollID", "EnrollID", assessmentGrade.EnrollID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", assessmentGrade.GradeItemID);
            return View(assessmentGrade);
        }

        // POST: AssessmentGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssessmentGradeID,Grade,Grade4,GradeP,GradeS,GradeItemID,EnrollID")] AssessmentGrade assessmentGrade)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assessmentGrade).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnrollID = new SelectList(db.Enrolls, "EnrollID", "EnrollID", assessmentGrade.EnrollID);
            ViewBag.GradeItemID = new SelectList(db.GradeItems, "GradeItemID", "Name", assessmentGrade.GradeItemID);
            return View(assessmentGrade);
        }

        // GET: AssessmentGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssessmentGrade assessmentGrade = db.AssessmentGrades.Find(id);
            if (assessmentGrade == null)
            {
                return HttpNotFound();
            }
            return View(assessmentGrade);
        }

        // POST: AssessmentGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssessmentGrade assessmentGrade = db.AssessmentGrades.Find(id);
            db.AssessmentGrades.Remove(assessmentGrade);
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
