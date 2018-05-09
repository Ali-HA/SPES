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
    public class GradeItemsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        //Upload Entrollement CSV file
        public ActionResult Upload()
        {
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(int GradeDistributionID, HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                TempData["fileName"] = path;
                TempData["GradeDistributionID"] = GradeDistributionID;
            }

            return RedirectToAction("Uploaded");
        }

        public ActionResult Uploaded()
        {
            if (TempData["fileName"] != null && TempData["GradeDistributionID"] != null)
            {
                string fileName = (string)TempData["fileName"];
                int GradeDistID = (int)TempData["GradeDistributionID"];
                var engine = new FileHelperEngine<GradeItemVM>();
                var records = engine.ReadFile(fileName);
                //List<GradeItem> enrols = new List<GradeItem>();
                int assessTypeId;
                foreach (var rec in records)
                {
                    assessTypeId = db.AssessmentTypes.Where(x => x.Type == rec.AssessType).FirstOrDefault().AssessmentTypeID;
                    var GrItem = new GradeItem() { AssessmentTypeID= assessTypeId, AssessmentTNo = rec.Number, CrNo = rec.Criterion, GradeDistributionID = GradeDistID, Weight = rec.Weight, Name = rec.Code};
                    db.GradeItems.Add(GrItem);
                    db.SaveChanges();
                    //enrols.Add(enrol);
                }
                //db.Enrollments.AddRange(enrols);
                //db.SaveChanges();

                return View(records);
            }
            return View();
        }

        // GET: GradeItems
        public ActionResult Index()
        {
            var gradeItems = db.GradeItems.Include(g => g.AssessmentType).Include(g => g.GradeDistribution);
            return View(gradeItems.ToList());
        }

        // GET: GradeItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeItem gradeItem = db.GradeItems.Find(id);
            if (gradeItem == null)
            {
                return HttpNotFound();
            }
            return View(gradeItem);
        }

        // GET: GradeItems/Create
        public ActionResult Create()
        {
            ViewBag.AssessmentTypeID = new SelectList(db.AssessmentTypes, "AssessmentTypeID", "Type");
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name");
            return View();
        }

        // POST: GradeItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GradeItemID,Name,AssessmentTypeID,AssessmentTNo,CrNo,Weight,GradeDistributionID")] GradeItem gradeItem)
        {
            if (ModelState.IsValid)
            {
                db.GradeItems.Add(gradeItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssessmentTypeID = new SelectList(db.AssessmentTypes, "AssessmentTypeID", "Type", gradeItem.AssessmentTypeID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", gradeItem.GradeDistributionID);
            return View(gradeItem);
        }

        // GET: GradeItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeItem gradeItem = db.GradeItems.Find(id);
            if (gradeItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssessmentTypeID = new SelectList(db.AssessmentTypes, "AssessmentTypeID", "Type", gradeItem.AssessmentTypeID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", gradeItem.GradeDistributionID);
            return View(gradeItem);
        }

        // POST: GradeItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GradeItemID,Name,AssessmentTypeID,AssessmentTNo,CrNo,Weight,GradeDistributionID")] GradeItem gradeItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gradeItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssessmentTypeID = new SelectList(db.AssessmentTypes, "AssessmentTypeID", "Type", gradeItem.AssessmentTypeID);
            ViewBag.GradeDistributionID = new SelectList(db.GradeDistributions, "GradeDistributionID", "Name", gradeItem.GradeDistributionID);
            return View(gradeItem);
        }

        // GET: GradeItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GradeItem gradeItem = db.GradeItems.Find(id);
            if (gradeItem == null)
            {
                return HttpNotFound();
            }
            return View(gradeItem);
        }

        // POST: GradeItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GradeItem gradeItem = db.GradeItems.Find(id);
            db.GradeItems.Remove(gradeItem);
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
