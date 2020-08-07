using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using ltweb.Helper;
using ltweb.Models;

namespace ltweb.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index()
        {
            var news = db.News.Include(n => n.Category).Include(n => n.Region).Include(n=>n.Images);
            return View(news.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName");
            ViewBag.CoverImage = new SelectList(db.NewsImages, "Id", "Image");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,SubDescription,CoverImage,DateTime,CategoryId,RegionId,UserId")] News news)
        {
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", news.CategoryId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", news.RegionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName",news.UserId);
            ViewBag.CoverImage = new SelectList(db.NewsImages, "Id", "Image",news.CoverImage);
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", news.CategoryId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", news.RegionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", news.UserId);
            ViewBag.CoverImage = new SelectList(db.NewsImages, "Id", "Image", news.CoverImage);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,SubDescription,CoverImage,DateTime,CategoryId,RegionId")] News news, HttpPostedFileBase cover)
        {
            if (ModelState.IsValid)
            {
                if (cover != null && cover.ContentLength > 0)
                {
                    string hash = MD5Helper.Calculate(cover);

                    string filename = Path.Combine(HttpContext.Server.MapPath("~/images"), hash + "_" + cover.FileName);
                    
                    cover.SaveAs(filename);

                    news.CoverImage = Path.Combine("\\images", hash + "_" + cover.FileName).Replace("\\", "/");
                }


                db.Entry(news).State = EntityState.Modified;    
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", news.CategoryId);
            ViewBag.RegionId = new SelectList(db.Regions, "Id", "Name", news.RegionId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", news.UserId);
            ViewBag.CoverImage = new SelectList(db.NewsImages, "Id", "Image", news.CoverImage); 
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
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
