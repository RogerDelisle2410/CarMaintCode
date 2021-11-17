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
    public class MaintenanceHistoriesController : Controller
    {
        private BCATPEntities1 db = new BCATPEntities1();

        // GET: MaintenanceHistories         

        public ActionResult Index(string searchString)
        {
            var maintenanceHistories = from s in db.MaintenanceHistories
                               select s;
            if (!string.IsNullOrEmpty(searchString))
            {
                maintenanceHistories = maintenanceHistories.Where(s => s.CustomerData.Name.Contains(searchString));
                 //ViewBag.Total = maintenanceHistories.Sum(x => x.Cost);
            }
            else
            {
                maintenanceHistories = db.MaintenanceHistories.Include(m => m.MaintenanceType).Include(m => m.CustomerData).Include(m => m.CarData);              
            }

            //var totals =
            //from s in maintenanceHistories
            //group s by s.CustId into grouped
            //select new
            //{
            //    SiteID = grouped.Key,
            //    Last30Sum = grouped.Sum(s => s.Cost)
            //};
            //foreach (var yyy in totals)
            //{
            //    ViewBag.Total = yyy.Last30Sum;
            //    return View(maintenanceHistories.ToList().OrderBy(t => t.Date));
            //}
            return View(maintenanceHistories.ToList().OrderBy(t => t.CustId));
        }

        // GET: MaintenanceHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceHistory maintenanceHistory = db.MaintenanceHistories.Find(id);
            if (maintenanceHistory == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistories/Create
        public ActionResult Create()
        {
            ViewBag.MaintId = new SelectList(db.MaintenanceTypes, "MaintId", "TaskName");
            ViewBag.CustId = new SelectList(db.CustomerDatas, "CustomerId", "Name");
            ViewBag.CarId = new SelectList(db.CarDatas, "CarId", "Manufacturer");
            return View();
        }

        // POST: MaintenanceHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,CarId,MaintId,Date,CustId,Cost")] MaintenanceHistory maintenanceHistory)
        {
            if (ModelState.IsValid)
            {
                db.MaintenanceHistories.Add(maintenanceHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaintId = new SelectList(db.MaintenanceTypes, "MaintId", "TaskName", maintenanceHistory.MaintId);
            ViewBag.CustId = new SelectList(db.CustomerDatas, "CustomerId", "Name", maintenanceHistory.CustId);
            ViewBag.CarId = new SelectList(db.CarDatas, "CarId", "Manufacturer", maintenanceHistory.CarId);
            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceHistory maintenanceHistory = db.MaintenanceHistories.Find(id);
            if (maintenanceHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaintId = new SelectList(db.MaintenanceTypes, "MaintId", "TaskName", maintenanceHistory.MaintId);
            ViewBag.CustId = new SelectList(db.CustomerDatas, "CustomerId", "Name", maintenanceHistory.CustId);
            ViewBag.CarId = new SelectList(db.CarDatas, "CarId", "Manufacturer", maintenanceHistory.CarId);
            return View(maintenanceHistory);
        }

        // POST: MaintenanceHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HistoryId,CarId,MaintId,Date,CustId,Cost")] MaintenanceHistory maintenanceHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(maintenanceHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaintId = new SelectList(db.MaintenanceTypes, "MaintId", "TaskName", maintenanceHistory.MaintId);
            ViewBag.CustId = new SelectList(db.CustomerDatas, "CustomerId", "Name", maintenanceHistory.CustId);
            ViewBag.CarId = new SelectList(db.CarDatas, "CarId", "Manufacturer", maintenanceHistory.CarId);
            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintenanceHistory maintenanceHistory = db.MaintenanceHistories.Find(id);
            if (maintenanceHistory == null)
            {
                return HttpNotFound();
            }
            return View(maintenanceHistory);
        }

        // POST: MaintenanceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MaintenanceHistory maintenanceHistory = db.MaintenanceHistories.Find(id);
            db.MaintenanceHistories.Remove(maintenanceHistory);
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
