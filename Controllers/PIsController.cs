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
    public class PIsController : Controller
    {
        private ACMDbContext db = new ACMDbContext();

        //Upload Entrollement CSV file
        public ActionResult Upload()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(int DepartmentID, HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);
                TempData["fileName"] = path;
                TempData["DepartmentID"] = DepartmentID;
            }

            return RedirectToAction("Uploaded");
        }

        public ActionResult Uploaded()
        {
            if (TempData["fileName"] != null && TempData["DepartmentID"] != null)
            {
                string fileName = (string)TempData["fileName"];
                int DepartmentID = (int)TempData["DepartmentID"];
                var engine = new FileHelperEngine<PIVM>();
                var records = engine.ReadFile(fileName);
                int LOId;
                foreach (var rec in records)
                {
                    LOId = db.LOs.Where(x => x.DepartmentID == DepartmentID && x.LearningOutcome == rec.SO).FirstOrDefault().LOID;
                    var pi = new PI() { LOID = LOId, PILetter=rec.PI, PIDesc=rec.desc, PINo=rec.Number };
                    db.PIs.Add(pi);
                    db.SaveChanges();
                }

                return View(records);
            }
            return View();
        }

        // GET: PIs
        public ActionResult Index()
        {
            var pIs = db.PIs.Include(p => p.LO);
            return View(pIs.ToList());
        }

        // GET: PIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PI pI = db.PIs.Find(id);
            if (pI == null)
            {
                return HttpNotFound();
            }
            return View(pI);
        }

        // GET: PIs/Create
        public ActionResult Create()
        {
            ViewBag.LOID = new SelectList(db.LOs, "LOID", "LearningOutcome");
            return View();
        }

        // POST: PIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PIID,LOID,PILetter,PINo,PIDesc")] PI pI)
        {
            if (ModelState.IsValid)
            {
                db.PIs.Add(pI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LOID = new SelectList(db.LOs, "LOID", "LearningOutcome", pI.LOID);
            return View(pI);
        }

        // GET: PIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PI pI = db.PIs.Find(id);
            if (pI == null)
            {
                return HttpNotFound();
            }
            ViewBag.LOID = new SelectList(db.LOs, "LOID", "LearningOutcome", pI.LOID);
            return View(pI);
        }

        // POST: PIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PIID,LOID,PILetter,PINo,PIDesc")] PI pI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LOID = new SelectList(db.LOs, "LOID", "LearningOutcome", pI.LOID);
            return View(pI);
        }

        // GET: PIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PI pI = db.PIs.Find(id);
            if (pI == null)
            {
                return HttpNotFound();
            }
            return View(pI);
        }

        // POST: PIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PI pI = db.PIs.Find(id);
            db.PIs.Remove(pI);
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
