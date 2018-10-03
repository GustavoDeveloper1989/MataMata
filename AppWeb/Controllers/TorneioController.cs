using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppWeb.Context;
using AppWeb.Models;

namespace AppWeb.Controllers
{
    public class TorneioController : Controller
    {
        private MataMataContext db = new MataMataContext();

        // GET: Torneio
        public ActionResult Index()
        {
            return View(db.TorneioModels.ToList());
        }

        // GET: Torneio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneioModel torneioModel = db.TorneioModels.Find(id);
            if (torneioModel == null)
            {
                return HttpNotFound();
            }
            return View(torneioModel);
        }

        // GET: Torneio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Torneio/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Qnt_Times")] TorneioModel torneioModel)
        {

            if (ModelState.IsValid)
            {

                var result = db.Times
                     .Select(m =>
                       new
                       {
                           Id = m.Id,
                           Name = m.Nome,
                           Escudo = m.EscudoUrl
                       }).ToList();


                if (result.Count >= torneioModel.Qnt_Times)
                {

                    db.TorneioModels.Add(torneioModel);
                    db.SaveChanges();

                }
                else {



                }

                return RedirectToAction("Index");
            }

            return View(torneioModel);
        }

        // GET: Torneio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneioModel torneioModel = db.TorneioModels.Find(id);
            if (torneioModel == null)
            {
                return HttpNotFound();
            }
            return View(torneioModel);
        }

        // POST: Torneio/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Qnt_Times")] TorneioModel torneioModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(torneioModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(torneioModel);
        }

        // GET: Torneio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TorneioModel torneioModel = db.TorneioModels.Find(id);
            if (torneioModel == null)
            {
                return HttpNotFound();
            }
            return View(torneioModel);
        }

        // POST: Torneio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TorneioModel torneioModel = db.TorneioModels.Find(id);
            db.TorneioModels.Remove(torneioModel);
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
