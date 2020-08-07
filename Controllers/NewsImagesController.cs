using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ltweb.Models;

namespace ltweb.Controllers
{
    public class NewsImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewsImages
        public ActionResult Index(string search = "", int newsId = 0)
        {
            var newsImages = db.NewsImages.Include(n => n.News).ToList();
            if (search != "")
                newsImages = newsImages.Where(pi => pi.Caption.ToLower().Contains(search.ToLower())).ToList();
            if (newsId > 0)
                newsImages = newsImages.Where(pi => pi.NewsId == newsId).ToList();
            ViewBag.Search = search;
            ViewBag.newsId = new SelectList(db.News, "Id", "Title", newsId);
            return View(newsImages.ToList());
        }

        // GET: NewsImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsImage newsImage = db.NewsImages.Find(id);
            if (newsImage == null)
            {
                return HttpNotFound();
            }
            return View(newsImage);
        }

        // GET: NewsImages/Create
        public ActionResult Create()
        {
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title");
            return View();
        }

        // POST: NewsImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Caption,Image,NewsId")] NewsImage newsImage)
        {
            if (ModelState.IsValid)
            {
                db.NewsImages.Add(newsImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsImage.NewsId);
            return View(newsImage);
        }

        // GET: NewsImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsImage newsImage = db.NewsImages.Find(id);
            if (newsImage == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsImage.NewsId);
            return View(newsImage);
        }

        // POST: NewsImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Caption,Image,NewsId")] NewsImage newsImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NewsId = new SelectList(db.News, "Id", "Title", newsImage.NewsId);
            return View(newsImage);
        }

        // GET: NewsImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsImage newsImage = db.NewsImages.Find(id);
            if (newsImage == null)
            {
                return HttpNotFound();
            }
            return View(newsImage);
        }

        // POST: NewsImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsImage newsImage = db.NewsImages.Find(id);
            db.NewsImages.Remove(newsImage);
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
