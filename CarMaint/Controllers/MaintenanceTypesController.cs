using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarMaint.Models;

namespace CarMaint.Controllers
{
    public class MaintenanceTypesController : Controller
    {
        private BCATPEntities1 db = new BCATPEntities1();

        // GET: MaintenanceTypes
        public ActionResult Index()
        {
            return View(db.MaintenanceTypes.ToList().OrderBy(t => t.TaskName));
        }

        // GET: MaintenanceTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceType maintenanceType = db.MaintenanceTypes.Find(id);
            if (maintenanceType == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceType);
        }

        // GET: MaintenanceTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaintId,TaskName,Cost,Gas,Diesel,Electric")] MaintenanceType maintenanceType)
        {
            if (ModelState.IsValid)
            {
                db.MaintenanceTypes.Add(maintenanceType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(maintenanceType);
        }

        // GET: MaintenanceTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceType maintenanceType = db.MaintenanceTypes.Find(id);
            if (maintenanceType == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceType);
        }

        // POST: MaintenanceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaintId,TaskName,Cost,Gas,Diesel,Electric")] MaintenanceType maintenanceType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(maintenanceType);
        }

        // GET: MaintenanceTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceType maintenanceType = db.MaintenanceTypes.Find(id);
            if (maintenanceType == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceType);
        }

        // POST: MaintenanceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceType maintenanceType = db.MaintenanceTypes.Find(id);
            db.MaintenanceTypes.Remove(maintenanceType);
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
