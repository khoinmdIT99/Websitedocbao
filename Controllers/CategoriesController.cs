using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ltweb.Models;
using Microsoft.AspNet.Identity.Owin;

namespace ltweb.Controllers
{
    //public class CustomActionInvoker : ControllerActionInvoker
    //{
    //    public override bool InvokeAction(ControllerContext controllerContext, string actionName)
    //    {
    //        if (FindAction(controllerContext, GetControllerDescriptor(controllerContext), actionName) == null)
    //        {
    //            return base.InvokeAction(controllerContext, "StrippedNameHandler");
    //        } 
    //        else
    //            return base.InvokeAction(controllerContext, actionName);
    //    }
    //}

    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;

        public ApplicationDbContext Context
        {
            get { return _context ?? Request.GetOwinContext().Get<ApplicationDbContext>(); }
            set { _context = value; }
        }

        //public CategoriesController()
        //{
        //    this.ActionInvoker = new CustomActionInvoker();
        //}

        // GET: Categories
        public ActionResult Index()
        {
            return View(Context.Categories.ToList());
        }

        //public async Task<ActionResult> StrippedNameHandler()
        //{
        //    string strippedName = Request.RequestContext.RouteData.GetRequiredString("action");
        //    //return Content(strippedName);
        //    Category cat = (await Context.Categories.ToListAsync()).FirstOrDefault(c => c.StrippedName == strippedName);
        //    if (cat == null)
        //        return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        //    ViewBag.Category = cat;
        //    return View("ListNews");
        //}

        // GET: Categories/News/{id}
        public async Task<ActionResult> GetNews(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category cat = (await Context.Categories.ToListAsync()).FirstOrDefault(c => int.TryParse(Id, out _) ? c.Id == int.Parse(Id) : c.StrippedName == Id);
            if (cat == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            ViewBag.Category = cat;
            return View("ListNews");
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                Context.Categories.Add(category);
                Context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                Context.Entry(category).State = EntityState.Modified;
                Context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = Context.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = Context.Categories.Find(id);
            foreach (News news in Context.News.Where(n => n.CategoryId == category.Id))
            {
                news.CategoryId = null;
            }

            Context.Categories.Remove(category);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    //if (disposing)
        //    //{
        //    //    Context.Dispose();
        //    //}
        //    base.Dispose(disposing);
        //}
    }
}
