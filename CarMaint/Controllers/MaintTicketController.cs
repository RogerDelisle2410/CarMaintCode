using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CarMaint.Models;
using System.Dynamic;

namespace CarMaint.Controllers
{
    public class MaintTicketController : Controller
    {
        private BCATPEntities1 db = new BCATPEntities1();
        public decimal theCost;
        public int maId;

        public class Article
        {
            public string Title { get; set; }
            public string Categories { get; set; }
        }
        // GET: NewMaint
        public ActionResult Index(string searchString)
        {
            var customer = from s in db.CustomerDatas
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                customer = customer.Where(s => s.Name.Contains(searchString));
            }
            return View(customer.ToList().OrderBy(t => t.Name));
        }

        public ActionResult Select(int id)
        {
            var cust = from cu in db.CustomerDatas
                       select cu;
            cust = cust.Where(cu => cu.CustomerId.Equals(id));

            foreach (var item in cust)
            {
                Session["CustomerName"] = item.Name;
                Session["CustomerId"] = item.CustomerId;
            }

            var car = from ca in db.CarDatas
                      select ca;
            car = car.Where(ca => ca.CustomerId.Equals(id));

            return View(car.ToList().OrderBy(t => t.Manufacturer));
        }

        public ActionResult SelectCar(string engineType, int carId)
        {
            var car = from ca in db.CarDatas
                      select ca;
            car = car.Where(ca => ca.CarId.Equals(carId));

            foreach (var item in car)
            {
                Session["CarId"] = item.CarId;
                Session["Manufacturer"] = item.Manufacturer;
                Session["Model"] = item.Model;
                Session["EngineType"] = item.EngineType;
                Session["Year"] = item.Year;
            }

            var maint = from ma in db.MaintenanceTypes
                        select ma;
            switch (engineType)
            {
                case "Gas":
                    maint = maint.Where(ma => ma.Gas.Equals(true));
                    break;
                case "Diesel":
                    maint = maint.Where(ma => ma.Diesel.Equals(true));
                    break;
                case "Electric":
                    maint = maint.Where(ma => ma.Electric.Equals(true));
                    break;
            }
            return View(maint.ToList().OrderBy(t => t.TaskName));
        }

        [HttpPost]
        public JsonResult AddToHistory(string[] arrayOfValues)
        {
            var cac = Session["CarId"];
            var cu = Session["CustomerId"];

            foreach (var it in arrayOfValues)
            {
                MaintenanceHistory maintenanceHistory = new MaintenanceHistory();
                var maId = Convert.ToInt16(it);
                var maintCost = from ma in db.MaintenanceTypes
                                select ma;
                maintCost = maintCost.Where(ma => ma.MaintId.Equals(maId));

                foreach (var item in maintCost)
                {
                    maintenanceHistory.Cost = Convert.ToDecimal(item.Cost);
                }

                maintenanceHistory.CarId = (int)cac;
                maintenanceHistory.CustId = (int)cu;
                maintenanceHistory.Date = DateTime.Now;
                maintenanceHistory.MaintId = Convert.ToInt16(maId);
                db.MaintenanceHistories.Add(maintenanceHistory);
                db.SaveChanges();
            }
            //return Json(arrayOfValues);
            return Json(new
            {
                redirectUrl = Url.Action("Index", "Home"),
                isRedirect = true
            });
        }
    }
}

       
 
 