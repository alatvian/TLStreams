using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TLStreamApp3.Models;

namespace TLStreamApp3.Controllers
{
    public class HomeController : Controller
    {
        private TLStreamApp3Context db = new TLStreamApp3Context();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.TLStreams.ToList());
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            TLStream tlstream = db.TLStreams.Find(id);
            if (tlstream == null)
            {
                return HttpNotFound();
            }
            return View(tlstream);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(TLStream tlstream)
        {
            if (ModelState.IsValid)
            {
                db.TLStreams.Add(tlstream);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tlstream);
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id = 0)
        {
            TLStream tlstream = db.TLStreams.Find(id);
            if (tlstream == null)
            {
                return HttpNotFound();
            }
            return View(tlstream);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(TLStream tlstream)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tlstream).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tlstream);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            TLStream tlstream = db.TLStreams.Find(id);
            if (tlstream == null)
            {
                return HttpNotFound();
            }
            return View(tlstream);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            TLStream tlstream = db.TLStreams.Find(id);
            db.TLStreams.Remove(tlstream);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}