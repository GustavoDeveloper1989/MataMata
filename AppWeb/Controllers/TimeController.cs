using AppWeb.Context;
using AppWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppWeb.Controllers
{
    public class TimeController : Controller
    {
        private MataMataContext db = new MataMataContext();

        // GET: Time
        public ActionResult Index()
        {

            List<TimeModel> times = db.Times.ToList();

            if (times == null)
            {
                return HttpNotFound();
            }
            return View(times);
        }

        [HttpGet]
        public ActionResult Create() {

            return View();

        }

        [HttpPost]
        public ActionResult Create(TimeModel time)
        {

            db.Times.Add(time);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            TimeModel time = db.Times
                     .Where(s => s.Id.ToString() == id)
                     .FirstOrDefault<TimeModel>();

            return View(time);

        }

        [HttpPost]
        public ActionResult Edit(TimeModel time)
        {

            db.Entry(time).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Details(string id)
        {

            TimeModel time = db.Times
                     .Where(s => s.Id.ToString() == id)
                     .FirstOrDefault<TimeModel>();

            return View(time);

        }

        [HttpGet]
        public ActionResult Delete(string id)
        {

            TimeModel time = db.Times 
                       .Where(s => s.Id.ToString() == id)
                       .FirstOrDefault<TimeModel>();

            return View(time);

        }

        [HttpPost]
        public ActionResult Delete(TimeModel time)
        {

            TimeModel time2 = db.Times
                       .Where(s => s.Id == time.Id)
                       .FirstOrDefault<TimeModel>();
            db.Times.Remove(time2);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

    }
}